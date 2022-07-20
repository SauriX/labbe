using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Application.IApplication;
using Service.Report.Dtos;
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
        [HttpGet("medicos/getAll")]
        public async Task<IEnumerable<MedicalStatsDto>> GetByDoctor()
        {
            return await _medicalstatsService.GetByDoctor();
        }

        [HttpPost("medicos/filter")]
        public async Task<IEnumerable<MedicalStatsDto>> GetDoctorNow(ReportFiltroDto search)
        {
            return await _medicalstatsService.GetFilter(search);
        }

        [HttpPost("medicos/download/pdf")]
        [AllowAnonymous]
        public async Task<IActionResult> DoctorPDF(ReportFiltroDto search)
        {
            var file = await _medicalstatsService.GenerateReportPDF(search);
            return File(file, MimeType.PDF, "Solic_MedicoCondensado.pdf");
        }
    }
}
