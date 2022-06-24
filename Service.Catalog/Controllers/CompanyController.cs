using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Catalog;
using Service.Catalog.Dtos.Company;
using Shared.Dictionary;
using System;
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
        public async Task<IEnumerable<CompanyListDto>> GetActive()
        {
            return await _Services.GetActive();
        }

        [HttpGet("{Id}")]
        [Authorize(Policies.Access)]
        public async Task<CompanyFormDto> GetById(Guid Id)
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
        public async Task<IActionResult> ExportList(string search)
        {
            var (file, fileName) = await _Services.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportForm(Guid id)
        {
            var (file, fileName) = await _Services.ExportForm(id);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpGet("paswwordgenerator")]
        public string GeneratePassword()
        {
            return _Services.GeneratePassword();
        }
    }
}
