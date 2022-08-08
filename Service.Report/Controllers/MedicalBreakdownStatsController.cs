using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Report.Dtos;
using Service.Report.Dtos.MedicalBreakdownStats;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    public partial class ReportController : ControllerBase
    {
        [HttpPost("medicos-desglosado/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<MedicalBreakdownRequestDto>> GetDoctorBreakdownNow(ReportFilterDto search)
        {
            return await _medicalbreakdownstatsService.GetByFilter(search);
        }
        [HttpPost("medicos-desglosado/table/filter")]
        [Authorize(Policies.Access)]
        public async Task<MedicalBreakdownDto> GetFilterMedicalBreakdownRequestTable(ReportFilterDto search)
        {
            return await _medicalbreakdownstatsService.GetTableByFilter(search);
        }
        [HttpPost("medicos-desglosado/chart/filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<MedicalBreakdownRequestChartDto>> GetMedicalBreakdownRequestFilterChart(ReportFilterDto search)
        {
            return await _medicalbreakdownstatsService.GetChartByFilter(search);
        }
        [HttpPost("medicos-desglosado/download/pdf")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> DoctorBreakdownPDF(ReportFilterDto search)
        {
            var file = await _medicalbreakdownstatsService.DownloadReportPdf(search);
            return File(file, MimeType.PDF, "Solic_MedicosDesglosados.pdf");
        }
    }
}
