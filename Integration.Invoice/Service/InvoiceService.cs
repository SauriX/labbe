using Facturapi;
using Integration.Invoice.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.Invoice.Service
{
    public class InvoiceService
    {
        const string API_KEY = "sk_test_jrKzRvqdg87nNoa6E0WBknQG7rJp1xlDwVyeGBAZ34";

        public static async Task<FacturapiDto> Create(FacturapiDto data)
        {
            var facturapi = new FacturapiClient(API_KEY);

            var client = data.Cliente;
            var products = data.Productos;

            var invoiceData = new Dictionary<string, object>
            {
                ["type"] = "I",
                ["payment_form"] = data.FormaPago,
                ["payment_method"] = data.MetodoPago,
                ["use"] = data.UsoCDFI,
                ["currency"] = "MXN",
                ["exchange"] = 1,
                ["external_id"] = data.ClaveExterna,
                ["series"] = "F",
                ["customer"] = new Dictionary<string, object>
                {
                    ["legal_name"] = client.RazonSocial,
                    ["tax_id"] = client.RFC,
                    ["tax_system"] = client.RegimenFiscal,
                    ["email"] = client.Correo,
                    ["phone"] = client.Telefono,
                    ["address"] = new Dictionary<string, object>
                    {
                        ["zip"] = client.Domicilio.CodigoPostal,
                        ["country"] = "MEX",
                        ["state"] = client.Domicilio.Estado,
                        ["city"] = client.Domicilio.Ciudad,
                        ["neighborhood"] = client.Domicilio.Colonia,
                        ["street"] = client.Domicilio.Calle,
                        ["interior"] = client.Domicilio.NumeroInterior,
                        ["exterior"] = client.Domicilio.NumeroExterior,
                    }
                },
                ["items"] = products.Select(x => new Dictionary<string, object>
                {
                    ["quantity"] = x.Cantidad,
                    ["discount"] = x.Descuento,
                    ["product"] = new Dictionary<string, object>
                    {
                        ["description"] = x.Descripcion,
                        ["product_key"] = x.ClaveProductoSAT,
                        ["price"] = x.Precio,
                        ["unit_key"] = x.ClaveUnidadSAT,
                        ["unit_name"] = x.ClaveUnidadNombreSAT,
                        ["sku"] = x.Clave
                    }
                }).ToList()
            };

            var invoice = await facturapi.Invoice.CreateAsync(invoiceData);

            data.FacturapiId = invoice.Id;

            return data;
        }

        public static async Task<Facturapi.Invoice> GetById(string invoiceId)
        {
            try
            {
                var facturapi = new FacturapiClient(API_KEY);

                var invoice = await facturapi.Invoice.RetrieveAsync(invoiceId);

                return invoice;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<byte[]> GetXml(string invoiceId)
        {
            try
            {
                var facturapi = new FacturapiClient(API_KEY);

                var invoice = await facturapi.Invoice.DownloadXmlAsync(invoiceId);

                return invoice.ToByteArray();
            }
            catch (Exception)
            {

                throw;
            }
        } 
        
        public static async Task<byte[]> GetPdf(string invoiceId)
        {
            try
            {
                var facturapi = new FacturapiClient(API_KEY);

                var invoice = await facturapi.Invoice.DownloadPdfAsync(invoiceId);

                return invoice.ToByteArray();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public static class Extensions
    {
        public static byte[] ToByteArray(this Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}