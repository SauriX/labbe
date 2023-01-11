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

namespace Service.MedicalRecord.Application
{
    public class InvoiceCompanyApplication : IInvoiceCompanyApplication
    {
        private readonly IRequestRepository _repository;
        private readonly IBillingClient _billingClient;

        public InvoiceCompanyApplication(IRequestRepository repository, IBillingClient billingClient)
        {
            _repository = repository;
            _billingClient = billingClient;
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

        public async Task<InvoiceCompanyInfoDto> GetByFilter(InvoiceCompanyFilterDto filter)
        {
            var request = await _repository.InvoiceCompanyFilter(filter);

            return request.ToInvoiceCompanyDto();
        }
        public async Task<string> GetNextPaymentNumber(string serie)
        {
            var date = DateTime.Now.ToString("yy");

            var lastCode = await _repository.GetLastPaymentCode(serie, date);
            var consecutive = lastCode == null ? 1 : Convert.ToInt32(lastCode.Replace(date, "")) + 1;

            return $"{date}{consecutive:D5}";
        }
       
    }
}
