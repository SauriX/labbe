using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.PriceList;
using Service.Catalog.Dtos.Promotion;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoController : ControllerBase
    {

        private readonly IPromotionApplication _service;

        public PromoController(IPromotionApplication indicationService)
        {
            _service = indicationService;
        }
        [HttpGet("all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<PromotionListDto>> GetAll(string search = null)
        {
            return await _service.GetAll(search);
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<PromotionFormDto> GetById(int id)
        {
            return await _service.GetById(id);
        }

        [HttpPost("info/study")]
        public async Task<List<PriceListInfoPromoDto>> GetStudyPromos(List<PriceListInfoFilterDto> filter)
        {
            return await _service.GetStudyPromos(filter);
        }

        [HttpPost("info/pack")]
        public async Task<List<PriceListInfoPromoDto>> GetPackPromos(List<PriceListInfoFilterDto> filter)
        {
            return await _service.GetPackPromos(filter);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<PromotionListDto> Create(PromotionFormDto branch)
        {
            branch.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Create(branch);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<PromotionListDto> Update(PromotionFormDto branch)
        {
            branch.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Update(branch);
        }

        [HttpPost("export/list/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListBranch(string search = null)
        {
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormBranch(int id)
        {
            var (file, fileName) = await _service.ExportForm(id);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpGet("active")]
        public async Task<IEnumerable<PromotionListDto>> GetActive()
        {
            return await _service.GetActive();
        }
    }
}
