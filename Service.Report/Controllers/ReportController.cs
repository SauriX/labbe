using Microsoft.AspNetCore.Mvc;
using Service.Report.Application.IApplication;

namespace Service.Report.Controllers.Report
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ReportController : ControllerBase
    {
        private readonly IPatientStatsApplication _patientstatsService;
        private readonly IRequestStatsApplication _requestService;
        private readonly IMedicalStatsApplication _medicalstatsService;
        private readonly IContactStatsApplication _contactstatsService;
        private readonly IStudyStatsApplication _studystatsService;
        private readonly IUrgentStatsApplication _urgentstatsService;

        public ReportController(
            IPatientStatsApplication patientStatsService,
            IRequestStatsApplication requestService,
            IMedicalStatsApplication medicalStatsService,
            IContactStatsApplication contactStatsService,
            IStudyStatsApplication studyStatsService,
            IUrgentStatsApplication urgentStatsService)
        {
            _patientstatsService = patientStatsService;
            _requestService = requestService;
            _medicalstatsService = medicalStatsService;
            _contactstatsService = contactStatsService;
            _studystatsService = studyStatsService;
            _urgentstatsService = urgentStatsService;
        }
    }
}
