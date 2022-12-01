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
        private readonly ICompanyStatsApplication _companystatsService;
        private readonly ICanceledRequestApplication _canceledrequestService;
        private readonly IMedicalBreakdownStatsApplication _medicalbreakdownstatsService;
        private readonly IDescountRequestApplication _descountrequestService;
        private readonly IChargeRequestApplication _chargerequestService;
        private readonly ICashRegisterApplication _cashregisterService;
        private readonly IMaquilaInternApplication _maquilainternService;
        private readonly IMaquilaExternApplication _maquilaexternService;
        private readonly IBudgetStatsApplication _budgetStatsService;

        public ReportController(
            IPatientStatsApplication patientStatsService,
            IRequestStatsApplication requestService,
            IMedicalStatsApplication medicalStatsService,
            IContactStatsApplication contactStatsService,
            IStudyStatsApplication studyStatsService,
            IUrgentStatsApplication urgentStatsService,
            ICompanyStatsApplication companyStatsService,
            IMedicalBreakdownStatsApplication medicalbreakdownstatsService,
            ICanceledRequestApplication canceledRequestService,
            IDescountRequestApplication descountRequestService,
            IChargeRequestApplication chargeRequestService,
            ICashRegisterApplication cashRegisterService,
            IMaquilaInternApplication maquilaInternService,
            IMaquilaExternApplication maquilaExternService,
            IBudgetStatsApplication budgetStatsService)
        {
            _patientstatsService = patientStatsService;
            _requestService = requestService;
            _medicalstatsService = medicalStatsService;
            _contactstatsService = contactStatsService;
            _studystatsService = studyStatsService;
            _urgentstatsService = urgentStatsService;
            _companystatsService = companyStatsService;
            _canceledrequestService = canceledRequestService;
            _medicalbreakdownstatsService = medicalbreakdownstatsService;
            _descountrequestService = descountRequestService;
            _chargerequestService = chargeRequestService;
            _cashregisterService = cashRegisterService;
            _maquilainternService = maquilaInternService;
            _maquilaexternService = maquilaExternService;
            _budgetStatsService = budgetStatsService;
        }
    }
}
