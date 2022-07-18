using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Application.IApplication;
using Service.Report.Dtos.MedicalStats;
using Service.Report.PdfModel;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpGet("medicos/getByDoctor")]
        public async Task<IEnumerable<MedicalStatsFiltroDto>> GetByDoctor()
        {
            return await _medicalstatsService.GetByDoctor();
        }

        [HttpPost("medicos/filter")]
        public async Task<IEnumerable<MedicalStatsFiltroDto>> GetNow(MedicalStatsSearchDto search)
        {
            return await _medicalstatsService.GetFilter(search);
        }

        [HttpPost("medicos/download/pdf")]
        [AllowAnonymous]
        public async Task<IActionResult> StatsPDF(MedicalStatsSearchDto search)
        {
            var file = await _medicalstatsService.GenerateReportPDF(search);
            return File(file, MimeType.PDF, "Solic_MedicoCondensado.pdf");
        }
    }
}
