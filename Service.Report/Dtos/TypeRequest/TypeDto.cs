using System.Collections.Generic;

namespace Service.Report.Dtos.TypeRequest
{
    public class TypeDto
    {
        public List<TypeRequestDto> TypeDescountRequest { get; set; }
        public InvoiceDto TypeDescountTotal { get; set; }
        public List<TypeRequestDto> TypeChargeRequest { get; set; }
        public InvoiceDto TypeChargeTotal { get; set; }
    }
}
