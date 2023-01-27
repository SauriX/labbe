using System;

namespace Service.Billing.Dtos.Invoice
{
    public class InvoiceSearch
    {
        public DateTime[] Fecha { get; set; }
        public string[] Sucursal { get; set; }
        public string Buscar { get; set; }
        public string Tipo { get; set; }
        public string Ciudad { get; set; }
    }
}
