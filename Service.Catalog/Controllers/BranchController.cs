using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Branch;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchApplication _branchService;

        public BranchController(IBranchApplication indicationService)
        {
            _branchService = indicationService;
        }

        [HttpGet("all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<BranchInfoDto>> GetAll(string search = null)
        {
            return await _branchService.GetAll(search);
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<BranchFormDto> GetById(string id)
        {
            return await _branchService.GetById(id);
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<bool> Create(BranchFormDto branch)
        {
            branch.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _branchService.Create(branch);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<bool> Update(BranchFormDto branch)
        {
            branch.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _branchService.Update(branch);
        }

        [HttpPost("export/list/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListBranch(string search = null)
        {
            var (file, fileName) = await _branchService.ExportListBranch(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormBranch(string id)
        {
            var (file, fileName) = await _branchService.ExportFormBranch(id);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpGet("getSucursalByCity")]
        public async Task<IEnumerable<BranchCityDto>> GetSucursalByCity()
        {
            return await _branchService.GetBranchByCity();
        }
    }
}
