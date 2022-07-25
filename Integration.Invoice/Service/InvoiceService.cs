using Facturapi;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integration.Invoice.Service
{
    public class InvoiceService
    {
        public static async Task<string> Create()
        {
            var facturapi = new FacturapiClient("sk_test_jrKzRvqdg87nNoa6E0WBknQG7rJp1xlDwVyeGBAZ34");

            try
            {
                //var facturapi = new FacturapiClient("sk_test_API_KEY");
                var invoiceP = facturapi.Invoice.CreateAsync(new Dictionary<string, object>
                {
                    ["customer"] = new Dictionary<string, object>
                    {
                        ["legal_name"] = "MIGUEL ALEJANDRO FARIAS ROCHA",
                        ["email"] = "email@example.com",
                        ["tax_id"] = "FARM960328BJ8",
                        ["tax_system"] = "626",
                        ["address"] = new Dictionary<string, object>
                        {
                            ["zip"] = "64102"
                        }
                    },
                    ["items"] = new Dictionary<string, object>[]
                  {
    new Dictionary<string, object>
    {
      ["product"] = new Dictionary<string, object>
      {
        ["description"] = "Ukelele",
        ["product_key"] = "60131324",
        ["price"] = 1.00
      }
    }
                  },
                    ["payment_form"] = Facturapi.PaymentForm.DINERO_ELECTRONICO,
                    ["folio_number"] = 914,
                    ["series"] = "F"
                });

                var invoice = await invoiceP;
                var a = invoice;

                return "Hola";
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}