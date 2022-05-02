﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Catalog;
using Service.Catalog.Dtos.Company;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identidad.Api.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyApplication _Services;

        public CompanyController(ICompanyApplication services)
        {
            _Services = services;
        }

        [HttpGet("all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<CompanyListDto>> GetAll(string search = null)
        {
            return await _Services.GetAll(search);
        }
        [HttpGet("active")]
        public async Task<IEnumerable<CompanyListDto>> GetActive(int id)
        {
            return await _Services.GetActive();
        }

        //[HttpGet("provenance/active")]
        //public async Task<IEnumerable<CatalogListDto>> GetActiveProvenance(int id)
        //{
        //    return await _Services.GetActive();
        //}

        [HttpGet("{Id}")]

        [HttpGet("{Id}")]
        [Authorize(Policies.Access)]
        public async Task<CompanyFormDto> GetById(int Id)
        {
            return await _Services.GetById(Id);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task Create(CompanyFormDto Company)
        {

            await _Services.Create(Company);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task Update(CompanyFormDto Company)
        {
            await _Services.Update(Company);
        }

        [HttpPost("export/list/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportList(string search = null)
        {
            var file = await _Services.ExportListCompany(search);
            return File(file, MimeType.XLSX);
        }

        [HttpPost("export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportForm(int id)
        {
            var file = await _Services.ExportFormCompany(id);
            return File(file, MimeType.XLSX);
        }

        [HttpGet("paswwordgenerator")]
        public async Task<string> GeneratePassword()
        {
            return await _Services.GeneratePassword();
        }
    }
}
