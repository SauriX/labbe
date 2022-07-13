﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Domain.Price;
using Service.Catalog.Dtos;
using Service.Catalog.Dtos.PriceList;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IPriceListApplication _service;

        public PriceController(IPriceListApplication service)
        {
            _service = service;
        }

        [HttpGet("active")]
        public async Task<IEnumerable<PriceListListDto>> GetActive()
        {
            return await _service.GetActive();
        }

        [HttpGet("all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<PriceListListDto>> GetAll(string search)
        {
            return await _service.GetAll(search);
        }

        [HttpGet("info/study/{id}")]
        [AllowAnonymous]
        public async Task<PriceListInfoStudyDto> GetPriceStudyById(int id)
        {
            return await _service.GetPriceStudyById(id, null, null, null);
        }

        [HttpGet("info/pack/{id}")]
        [AllowAnonymous]
        public async Task<PriceListInfoPackDto> GetPricePackById(int id)
        {
            return await _service.GetPricePackById(id, null, null, null);
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<PriceListFormDto> GetById(string id)
        {
            return await _service.GetById(id);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<PriceListListDto> Create(PriceListFormDto price)
        {
            price.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Create(price);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<PriceListListDto> Update(PriceListFormDto price)
        {
            price.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Update(price);
        }

        [HttpPost("export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListPriceList(string search)
        {
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormPriceList(string id)
        {
            var (file, fileName) = await _service.ExportForm(id);
            return File(file, MimeType.XLSX, fileName);
        }
        [HttpGet("branch")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<PriceListBranchDto>> GetAllBranch(Guid branchId)
        {
            return await _service.GetAllBranch(branchId);
        }
        [HttpGet("medics")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<PriceListMedicDto>> GetAllMedics(Guid medicsId)
        {
            return await _service.GetAllMedics(medicsId);
        }
        [HttpGet("company")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<PriceListCompanyDto>> GetAllCompany(Guid companyId)
        {
            return await _service.GetAllCompany(companyId);
        }
    }
}
