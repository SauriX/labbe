using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Report.Application.IApplication;

using Microsoft.AspNetCore.Http;
using Shared.Error;
using Shared.Helpers;
using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

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
        private readonly IIndicatorsStatsApplication _indicatorsStatsService;
        private readonly ILogger<ReportController> _logger;

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
            IBudgetStatsApplication budgetStatsService,
            IIndicatorsStatsApplication indicatorsStatsService,
            ILogger<ReportController> logger)
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
            _indicatorsStatsService = indicatorsStatsService;
            _logger = logger;
        }
    }
}
