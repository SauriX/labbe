using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers.Catalog
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("costofijo/all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<BudgetListDto>> GetAllBudget(string search)
        {
            return await _budgetService.GetAll(search);
        }

        [HttpGet("costofijo/active")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<BudgetListDto>> GetActiveBudget()
        {
            return await _budgetService.GetActive();
        }

        [HttpGet("costofijo/branch/{branchId}/active")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<BudgetListDto>> GetBudgetByBranch(Guid branchId)
        {
            return await _budgetService.GetBudgetByBranch(branchId);
        }
        
        [HttpPost("costofijo/getBranches")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<BudgetListDto>> GetBudgetsByBranch(BudgetFilterDto search)
        {
            return await _budgetService.GetBudgetsByBranch(search);
        }

        [HttpGet("costofijo/{id}")]
        [Authorize(Policies.Access)]
        public async Task<BudgetFormDto> GetBudgetById(int id)
        {
            return await _budgetService.GetById(id);
        }

        [HttpPost("costofijo")]
        [Authorize(Policies.Create)]
        public async Task<BudgetListDto> CreateBudget(BudgetFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _budgetService.Create(catalog);
        }
        
        [HttpPost("costofijo/list")]
        [Authorize(Policies.Create)]
        public async Task CreateListOfBudget(List<BudgetFormDto> catalogs)
        {
            //catalogs.UsuarioId = (Guid)HttpContext.Items["userId"];
            await _budgetService.CreateList(catalogs);
        }

        [HttpPut("costofijo")]  
        [Authorize(Policies.Update)]
        public async Task<BudgetListDto> UpdateBudget(BudgetFormDto catalog)
        {
            catalog.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _budgetService.Update(catalog);
        }
        
        [HttpPut("costofijo/update")]  
        [Authorize(Policies.Update)]
        public async Task UpdateService(ServiceUpdateDto catalog)
        {
            var userId = (Guid)HttpContext.Items["userId"];
            await _budgetService.UpdateService(catalog, userId);
        }

        [HttpPost("costofijo/export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportListBudget(string search)
        {
            var file = await _budgetService.ExportList(search);
            return File(file, MimeType.XLSX, "Catálogo de Costos Fijos.xlsx");
        }

        [HttpPost("costofijo/export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportFormBudget(int id)
        {
            var (file, code) = await _budgetService.ExportForm(id);

            return File(file, MimeType.XLSX, $"Catálogo de Costos Fijos ({code}).xlsx");
        }
    }
}
