using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Promotion;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {

        private readonly IPromotionApplication _Service;

        public PromotionController(IPromotionApplication indicationService)
        {
            _Service = indicationService;
        }
        [HttpGet("all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<PromotionListDto>> GetAll(string search = null)
        {
            return await _Service.GetAll(search);
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<PromotionFormDto> GetById(int id)
        {
            return await _Service.GetById(id);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<PromotionFormDto> Create(PromotionFormDto branch)
        {
            branch.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _Service.Create(branch);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<PromotionListDto> Update(PromotionFormDto branch)
        {
            branch.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _Service.Update(branch);
        }

        [HttpPost("export/list/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListBranch(string search = null)
        {
            var (file, fileName) = await _Service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormBranch(int id)
        {
            var (file, fileName) = await _Service.ExportForm(id);
            return File(file, MimeType.XLSX, fileName);
        }
    }
}
