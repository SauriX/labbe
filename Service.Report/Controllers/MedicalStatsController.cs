using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos;
using Service.Report.Dtos.MedicalStats;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpPost("medicos/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<MedicalStatsDto>> GetDoctorNow(ReportFilterDto search)
        {
            return await _medicalstatsService.GetByFilter(search);
        }

        [HttpPost("medicos/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> DoctorPDF(ReportFilterDto search)
        {
            var file = await _medicalstatsService.DownloadReportPdf(search);
            return File(file, MimeType.PDF, "Solic_MedicoCondensado.pdf");
        }
    }
}
