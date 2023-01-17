using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Invoice.Dtos
{
    public class InvoiceCancelation
    {
        public string FacturapiId { get; set; }
        public string Motivo { get; set; }
    }
}