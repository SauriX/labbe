using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.Invoice;
using Service.MedicalRecord.Dtos.InvoiceCompany;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Domain.Invoice;
using EventBus.Messages.Common;
using RequestTemplates = Service.MedicalRecord.Dictionary.EmailTemplates.Request;
using MassTransit;
using Service.MedicalRecord.Settings.ISettings;
using System.IO;
using Microsoft.Extensions.Configuration;
using Shared.Error;
using System.Net;
using SharedResponses = Shared.Dictionary.Responses;

namespace Service.MedicalRecord.Application
{
    public class InvoiceCompanyApplication : IInvoiceCompanyApplication
    {
        private readonly IRequestRepository _repository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IBillingClient _billingClient;
        private readonly ICatalogClient _catalogClient;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IRabbitMQSettings _rabbitMQSettings;
        private readonly IQueueNames _queueNames;
        private readonly string InvoiceCompanyPath;
        private readonly IPdfClient _pdfClient;

        public InvoiceCompanyApplication(
            IMedicalRecordRepository medicalRecordRepository,
            IRequestRepository repository,
            IInvoiceRepository invoiceRepository,
            IBillingClient billingClient,
            ICatalogClient catalogClient,
            ISendEndpointProvider sendEndpoint,
            IRabbitMQSettings rabbitMQSettings,
            IQueueNames queueNames,
            IConfiguration configuration,
            IPdfClient pdfClient,
            IRequestRepository requestRepository
            )
        {
            _medicalRecordRepository = medicalRecordRepository;
            _repository = repository;
            _catalogClient = catalogClient;
            _invoiceRepository = invoiceRepository;
            _billingClient = billingClient;
            _sendEndpointProvider = sendEndpoint;
            _queueNames = queueNames;
            _rabbitMQSettings = rabbitMQSettings;
            InvoiceCompanyPath = configuration.GetValue<string>("ClientUrls:MedicalRecord") + configuration.GetValue<string>("ClientRoutes:MedicalRecord");
            _pdfClient = pdfClient;
            _requestRepository = requestRepository;


        }

        public async Task<InvoiceDto> CheckInPayment(InvoiceCompanyDto invoice)
        {

            InvoiceDto invoiceDto = new InvoiceDto
            {
                FormaPago = invoice.FormaPago,
                MetodoPago = invoice.MetodoPago,
                UsoCFDI = invoice.UsoCFDI,
                RegimenFiscal = invoice.RegimenFiscal,
                RFC = invoice.RFC,
                Cliente = new ClientDto
                {
                    RazonSocial = invoice.Cliente.RazonSocial,
                    RFC = invoice.Cliente.RFC,
                    RegimenFiscal = invoice.Cliente.RegimenFiscal,
                    Correo = invoice.Cliente.Correo,
                    Telefono = invoice.Cliente.Telefono,
                    CodigoPostal = invoice.Cliente.CodigoPostal,
                    Calle = invoice.Cliente.Calle,
                    NumeroExterior = invoice.Cliente.NumeroExterior,
                    NumeroInterior = "",
                    Colonia = invoice.Cliente.Colonia,
                    Ciudad = invoice.Cliente.Ciudad,
                    Municipio = invoice.Cliente.Municipio,
                    Estado = invoice.Cliente.Estado,
                    Pais = invoice.Cliente.Pais,
                },


                Productos = invoice.Estudios.Select(x => new ProductDto
                {
                    Clave = x.Clave,
                    Descripcion = x.Estudio,
                    Precio = x.Precio,
                    Descuento = x.Descuento,
                    Cantidad = 1,

                }).ToList(),

            };

            //var invoiceResponse = await _billingClient.CheckInPayment(invoiceDto);

            return await _billingClient.CheckInPayment(invoiceDto);
        }

        public async Task<InvoiceDto> CheckInPaymentCompany(InvoiceCompanyDto invoice)
        {

            InvoiceDto invoiceDto = new InvoiceDto
            {
                FormaPago = invoice.FormaPago,
                MetodoPago = invoice.MetodoPago,
                UsoCFDI = invoice.UsoCFDI,
                Serie = invoice.Serie,
                RegimenFiscal = invoice.RegimenFiscal,
                RFC = invoice.RFC,
                SolicitudesId = invoice.SolicitudesId,
                Cliente = new ClientDto
                {
                    RazonSocial = invoice.Cliente.RazonSocial,
                    RFC = invoice.Cliente.RFC,
                    RegimenFiscal = invoice.Cliente.RegimenFiscal,
                    Correo = invoice.Cliente.Correo,
                    Telefono = invoice.Cliente.Telefono,
                    CodigoPostal = invoice.Cliente.CodigoPostal,
                    Calle = invoice.Cliente.Calle,
                    NumeroExterior = invoice.Cliente.NumeroExterior,
                    NumeroInterior = "",
                    Colonia = invoice.Cliente.Colonia,
                    Ciudad = invoice.Cliente.Ciudad,
                    Municipio = invoice.Cliente.Municipio,
                    Estado = invoice.Cliente.Estado,
                    Pais = invoice.Cliente.Pais,
                },


                Productos = invoice.Detalles.Select(x => new ProductDto
                {
                    ClaveProdServ = x.ClaveProdServ,
                    Clave = string.IsNullOrEmpty(x.EstudioClave) ? x.Id.ToString() : x.EstudioClave,
                    Descripcion = x.Concepto,
                    Precio = x.Importe,
                    Descuento = x.Descuento,
                    Cantidad = x.Cantidad,

                }).ToList(),

            };

            var invoiceResponse = await _billingClient.CheckInPaymentCompany(invoiceDto);

            var invoiceCompany = invoice.ToInvoiceCompany(invoiceResponse, invoice);

            List<RequestInvoiceCompany> requestsInvoiceCompany = new();

            foreach (var solicitud in invoice.SolicitudesId)
            {
                requestsInvoiceCompany.Add(new RequestInvoiceCompany    
                {
                    Activo = true,
                    SolicitudId = solicitud,
                    InvoiceCompanyId = invoiceCompany.Id,

                });

            }

            await _invoiceRepository.CreateInvoiceCompanyData(invoiceCompany, requestsInvoiceCompany);

            return invoiceResponse;
        }
        public async Task CheckInInvoiceGlobal(List<Guid> requests)
        {
            List<InvoiceDto> invoices = new List<InvoiceDto>();

            List<Domain.Request.Request> solicitudes = await _requestRepository.GetRequestsByListId(requests);

            foreach (var solicitud in solicitudes)
            {
                var pago = solicitud.Pagos.OrderBy(x => x.Cantidad).FirstOrDefault();

                //Domain.MedicalRecord.MedicalRecord cliente = await _medicalRecordRepository.GetById(solicitud.ExpedienteId);
                Domain.MedicalRecord.MedicalRecord cliente = solicitud.Expediente;

                List<Guid> solicitudesId = new List<Guid>{ solicitud.Id };

                invoices.Add(new InvoiceDto
                {
                    FormaPago = pago.FormaPago,
                    MetodoPago = "PUE",
                    UsoCFDI = "G03",
                    Serie = "",
                    RegimenFiscal = cliente.TaxData.ToList().FirstOrDefault().Factura.RegimenFiscal,
                    RFC = cliente.TaxData.ToList().FirstOrDefault().Factura.RFC,
                    SolicitudesId = solicitudesId,
                    Cliente = new ClientDto
                    {
                        RazonSocial = cliente.TaxData.ToList().FirstOrDefault().Factura.RazonSocial,
                        RFC = cliente.TaxData.ToList().FirstOrDefault().Factura.RFC,
                        RegimenFiscal = cliente.TaxData.ToList().FirstOrDefault().Factura.RegimenFiscal,
                        Correo = "",
                        Telefono = "",
                        CodigoPostal = cliente.TaxData.ToList().FirstOrDefault().Factura.CodigoPostal,
                        Calle = cliente.TaxData.ToList().FirstOrDefault().Factura.Calle,
                        NumeroExterior = "",
                        NumeroInterior = "",
                        Colonia = "",
                        Ciudad = cliente.TaxData.ToList().FirstOrDefault().Factura.Ciudad,
                        Municipio = "",
                        Estado = "",
                        Pais = "",
                    },

                    Productos = solicitud.Estudios.Select(x => new ProductDto
                    {
                        ClaveProdServ = "85121800",
                        Clave = x.Clave,
                        Descripcion = x.Nombre,
                        Precio = x.PrecioFinal,
                        Descuento = x.Descuento,
                        Cantidad = 1,

                    }).ToList(),
                });
            
            }


            
            throw new NotImplementedException();
        }
        public async Task<InvoiceCompanyDto> GetById(string invoiceId)
        {
            var existing = await _invoiceRepository.GetInvoiceById(invoiceId);
            var invoiceData = existing.ToInvoiceDto();
            return invoiceData;
        }

        public async Task<string> Cancel(InvoiceCancelation invoiceDto)
        {
            var resposeBilling = await _billingClient.CancelInvoice(invoiceDto);


            if (resposeBilling.ToLower() == "canceled")
            {
                var invoice = await _invoiceRepository.GetInvoiceCompanyByFacturapiId(invoiceDto.FacturapiId);

                invoice.Estatus = "Cancelado";

                await _invoiceRepository.UpdateInvoiceCompany(invoice);

            }

            return resposeBilling;
        }

        public async Task<bool> EnvioFactura(InvoiceCompanyDeliverDto envio)
        {
            List<SenderFiles> files = new List<SenderFiles>();
            

                var factura = await _billingClient.DownloadPDF(envio.FacturapiId);

                string namePdf = string.Concat(envio.FacturapiId, ".pdf");

                var pathInvoice = await SaveInvoicePdfPath(factura, namePdf);

                var pathName = Path.Combine(InvoiceCompanyPath, pathInvoice.Replace("wwwroot/", "")).Replace("\\", "/");

                files = new List<SenderFiles>()
                            {
                                new SenderFiles(new Uri(pathName), namePdf)
                            };
           
            
            foreach (var contacto in envio.Contactos)
            {
                if (envio.MediosEnvio.Contains("whatsapp") || envio.MediosEnvio.Contains("ambos"))
                {
                    await SendInvoiceWhatsapp(files, contacto.Telefono, envio.UsuarioId, "", envio.FacturapiId, envio.EsPrueba);

                }
                if (envio.MediosEnvio.Contains("correo") || envio.MediosEnvio.Contains("ambos"))
                {
                    await SendInvoiceEmail(files, contacto.Correo, envio.UsuarioId, "", envio.FacturapiId, envio.EsPrueba);

                }
            }
            return true;
        }
        public static async Task<string> SaveInvoicePdfPath(byte[] pdf, string name)
        {
            var path = "wwwroot/temp/Invoices";

            await File.WriteAllBytesAsync(Path.Combine(path, name), pdf);

            return Path.Combine(path, name);
        }
        public async Task SendInvoiceEmail(List<SenderFiles> senderFiles, string correo, Guid usuario, string nombrePaciente, Guid claveSolicitud, bool esPrueba)
        {

            var subject = RequestTemplates.Subjects.PathologicalSubject;
            var title = RequestTemplates.Titles.PathologicalTitle;
            var message = esPrueba ? RequestTemplates.Messages.TestMessage : $"{nombrePaciente}, para LABPRATORIOS RAMOS ha sido un placer atenderte, a continuación se brindan los resultados de la solicitud ${claveSolicitud}\n" +
                "\n" +
                "Te recordamos que también puedes descargar tu resultados desde nuestra página web https://www.laboratorioramos.com.mx necesitaras tu número de expediente y contraseña proporcionados en tu recibo de pago."; ;


            var emailToSend = new EmailContract(correo, subject, title, message, senderFiles)
            {
                Notificar = true,
                RemitenteId = usuario.ToString()
            };

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(string.Concat(_rabbitMQSettings.Host, "/", _queueNames.Email)));

            await endpoint.Send(emailToSend);


        }

        public async Task SendInvoiceWhatsapp(List<SenderFiles> senderFiles, string telefono, Guid usuario, string nombrePaciente, Guid claveSolicitud, bool esPrueba)
        {

            var message = esPrueba ? RequestTemplates.Messages.TestMessage : $"{nombrePaciente}, para LABPRATORIOS RAMOS ha sido un placer atenderte, a continuación se brindan los resultados de la solicitud {claveSolicitud}\n" +
                "\n" +
                "Te recordamos que también puedes descargar tu resultados desde nuestra página web https://www.laboratorioramos.com.mx necesitaras tu número de expediente y contraseña proporcionados en tu recibo de pago.";


            var phone = telefono.Replace("-", "");
            phone = phone.Length == 10 ? "52" + phone : phone;

            var emailToSend = new WhatsappContract(phone, message, esPrueba ? null : senderFiles)
            {
                Notificar = true,
                RemitenteId = usuario.ToString()
            };

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(string.Concat(_rabbitMQSettings.Host, "/", _queueNames.Whatsapp)));

            await endpoint.Send(emailToSend);


        }

        public async Task<InvoiceCompanyInfoDto> GetByFilter(InvoiceCompanyFilterDto filter)
        {
            var request = await _invoiceRepository.InvoiceCompanyFilter(filter);

            return request.ToInvoiceCompanyDto();
        }
        public async Task<List<InvoiceFreeDataDto>> GetByFilterFree(InvoiceFreeFilterDto filter)
        {
            var invoices = await _invoiceRepository.InvoiceFreeFilter(filter);

            return invoices.ToInvoicesFreeDataDto();
        }
        public async Task<string> GetNextPaymentNumber(string serie)
        {
            var date = DateTime.Now.ToString("yy");

            var lastCode = await _repository.GetLastPaymentCode(serie, date);
            var consecutive = lastCode == null ? 1 : Convert.ToInt32(lastCode.Replace(date, "")) + 1;

            return $"{date}{consecutive:D5}";
        }

        public async Task<byte[]> PrintTicket(ReceiptCompanyDto receipt)
        {
            var requestStudies = await _repository.GetRequestsStudyByListId(receipt.SolicitudesId);

            var requests = await _repository.GetRequestsByListId(receipt.SolicitudesId);

            if (requestStudies == null || requests == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            var order = receipt.ToRequestTicketDto(requestStudies, requests);

            return await _pdfClient.GenerateInvoiceCompanyTicket(order);
        }

        
    }
}
