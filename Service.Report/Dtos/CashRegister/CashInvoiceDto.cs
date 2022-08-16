using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.CashRegister
{
    public class CashInvoiceDto
    {
        public decimal SumaEfectivo { get; set; }
        public decimal SumaTDC { get; set; }
        public decimal SumaTransferencia { get; set; }
        public decimal SumaCheque { get; set; }
        public decimal SumaTDD { get; set; }
        public decimal Total => SumaEfectivo + SumaTDC + SumaTransferencia + SumaCheque + SumaTDD;
    }
}
