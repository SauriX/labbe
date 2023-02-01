using ClosedXML.Excel;
using ClosedXML.Report;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Dtos.Invoice;
using Service.MedicalRecord.Dtos.InvoiceCatalog;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository;
using Service.MedicalRecord.Repository.IRepository;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application
{
    public class InvoiceCatalogApplication: IInvoiceCatalogApplication
    {
        private readonly IInvoiceCatalogRepository _repository;
        private readonly IBillingClient _billingClient;



        public InvoiceCatalogApplication(IInvoiceCatalogRepository repository,IBillingClient billingClient)
        {
            _repository = repository;
            _billingClient = billingClient;
        }

        public async Task<List<InvoiceCatalogList>> getAll(InvoiceCatalogSearch search) {
            
            List<InvoiceCatalogList> list = new List<InvoiceCatalogList>();
            var notas = await _repository.GetNotas(search);
            list.AddRange(notas.ToInvoiceList());
            var facturas = await getFacturas(search);
            list.AddRange(facturas);
            if (!string.IsNullOrEmpty(search.Tipo)) {
                list = list.FindAll(x=> x.Tipo == search.Tipo);
            }
            return list;
        }

        public async Task<List<InvoiceCatalogList>> getFacturas(InvoiceCatalogSearch search) {
            
            var facturas = await _billingClient.getAllInvoice(search);
            var facturasQ = facturas.AsQueryable();
            facturas = facturasQ.ToList();
                var invoices = facturas.ToInvoiceList();
            List<string> nSolicitudes = new List<string>();
            foreach (var invoice in invoices)
            {

                nSolicitudes.Add(invoice.Solicitud);
            }
            var solicitudes = await _repository.GetSolicitudbyclave(nSolicitudes);
            List<InvoiceCatalogList> invoiceList = new List<InvoiceCatalogList>();
            foreach (var invoice in invoices) {
                var solicitud = solicitudes.Find(x=>x.Clave == invoice.Solicitud);
                invoice.SucursalId = solicitud.SucursalId.ToString();
                invoice.Compañia = solicitud.Compañia.Nombre;
                invoice.Ciudad = solicitud.Sucursal.Ciudad;
                invoiceList.Add(invoice);
            }
            var invoicesQ = invoiceList.AsQueryable();
            if (search.Sucursal !=null  && search.Sucursal?.Length>0)
            {
                invoicesQ = invoicesQ.Where(x => search.Sucursal.Any(y=> y==x.SucursalId.ToString() ));
            }

            if (search.Ciudad !=null)
            {
                if (search.Ciudad?.Length > 0) {
                    invoicesQ = invoicesQ.Where(x => search.Ciudad.Contains(x.Ciudad));
                }
                
            }

            return invoicesQ.ToList();
        }
        public async Task<(byte[] file, string fileName)> ExportList(InvoiceCatalogSearch search)
        {
            var invoice= await getAll(search);

            var path = Assets.InvoiceList;

            var template = new XLTemplate(path);
            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Catálogo de Facturas y Notas");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Invoice", invoice);

            template.Generate();
            var range = template.Workbook.Worksheet("Invoice").Range("Invoice");
            var table = template.Workbook.Worksheet("Invoice").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;
            template.Format();

            return (template.ToByteArray(), $"Catálogo de Facturas y Notas.xlsx");
        }
    }
}
