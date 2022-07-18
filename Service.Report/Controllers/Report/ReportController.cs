using Microsoft.AspNetCore.Mvc;
using Service.Report.Application.IApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Controllers.Report
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ReportController : ControllerBase
    {
        private readonly IPatientStatsApplication _patientstatsService;
        private readonly IRequestApplication _requestService;
        private readonly IMedicalStatsApplication _medicalstatsService;

        public ReportController(
            IPatientStatsApplication patientStatsService, 
            IRequestApplication requestService,
            IMedicalStatsApplication medicalstatsService)
        {
            _patientstatsService = patientStatsService;
            _requestService = requestService;
            _medicalstatsService = medicalstatsService;
        }
    }
}
