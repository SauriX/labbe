using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers.Catalog
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("workList/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllWorkList(string search = null)
        {
            return await _workListService.GetAll(search);
        }

        [HttpGet("workList/{id}")]
        public async Task<CatalogFormDto> GetWorkListById(int id)
        {
            return await _workListService.GetById(id);
        }

        [HttpPost("workList")]
        public async Task<CatalogListDto> CreateWorkList(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _workListService.Create(catalog);
        }

        [HttpPut("workList")]
        public async Task<CatalogListDto> UpdateWorkList(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            return await _workListService.Update(catalog);
        }
    }
}