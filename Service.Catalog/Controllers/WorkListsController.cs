using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    public partial class CatalogController : ControllerBase
    {

        [HttpGet("workLists/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllWorkLists(string search = null)
        {
            return await _workListsService.GetAll(search);
        }

        [HttpGet("workLists/{id}")]
        public async Task<CatalogFormDto> GetWorkListsById(int id)
        {
            return await _workListsService.GetById(id);
        }

        [HttpPost("workLists")]
        public async Task CreateWorkLists(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _workListsService.Create(catalog);
        }

        [HttpPut("workLists")]
        public async Task UpdateWorkLists(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _workListsService.Update(catalog);
        }
    }
}