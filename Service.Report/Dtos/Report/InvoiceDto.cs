using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.Report
{
    public class InvoiceDto
    {
        public decimal Subtotal => Total - IVA;
        public decimal IVA => Total * (decimal)0.16;
        public decimal Total { get; set; }
    }
}
