﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.MassSearch;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MassSearchController : ControllerBase
    {
        private readonly IMassSearchApplication _service;

        public MassSearchController(IMassSearchApplication service)
        {
            _service = service;
        }

        [HttpPost("GetByFilter")]
        [Authorize(Policies.Access)]
        public async Task<MassSearchInfoDto> GetByFilter(MassSearchFilterDto filter)
        {
            return await _service.GetByFilter(filter);
        }

        [HttpPost("GetAllCaptureResults")]
        [Authorize(Policies.Access)]
        public async Task<List<RequestsInfoDto>> GetAllCaptureResults(DeliverResultsFilterDto search)
        {
            var clinicResults = await _service.GetAllCaptureResults(search);
            return clinicResults;
        }

    }
}
