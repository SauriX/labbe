using Service.Report.Dtos.CompanyStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.CanceledRequest
{
    public class CanceledDto
    {
        public List<CanceledRequestDto> CanceledRequest { get; set; }
        public InvoiceDto CanceledTotal { get; set; }
    }
}
