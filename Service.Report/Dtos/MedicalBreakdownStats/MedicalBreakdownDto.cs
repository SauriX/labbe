using System.Collections.Generic;

namespace Service.Report.Dtos.MedicalBreakdownStats
{
    public class MedicalBreakdownDto
    {
        public List<MedicalBreakdownRequestDto> MedicalBreakdownRequest { get; set; }
        public InvoiceDto MedicalBreakdownTotal { get; set; }
    }
}
