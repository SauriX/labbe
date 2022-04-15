using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Branch;
using Shared.Dictionary;
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

        [HttpPost]
        public async Task<bool> Create(BranchForm branch) {
            return await _branchService.Create(branch);
        }
        [HttpPut]
        public async Task<bool> Update(BranchForm branch)
        {
            return await _branchService.Update(branch);
        }
        [HttpGet("{id}")]
        public async Task<BranchForm> GetById(string id)
        {
            return await _branchService.GetById(id);
        }

        [HttpGet("all/{search?}")]
        public async Task<IEnumerable<BranchInfo>> GetAll(string search = null)
        {
            return await _branchService.GetAll(search);
        }

        [HttpPost("export/list/{search?}")]
        public async Task<IActionResult> ExportListBranch(string search = null)
        {
            var file = await _branchService.ExportListBranch(search);
            return File(file, MimeType.XLSX);
        }

        [HttpPost("export/form/{id}")]
        public async Task<IActionResult> ExportFormBranch(string id)
        {
            var file = await _branchService.ExportFormBranch(id);
            return File(file, MimeType.XLSX);
        }
    }
}
