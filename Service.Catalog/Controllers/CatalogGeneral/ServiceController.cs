using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("service/all/{search?}")]
        public async Task<IEnumerable<CatalogListDto>> GetAllService(string search = null)
        {
            return await _serviceService.GetAll(search);
        }

        [HttpGet("service/{id}")]
        public async Task<CatalogFormDto> GetServiceById(int id)
        {
            return await _serviceService.GetById(id);
        }

        [HttpPost("service")]
        public async Task CreateService(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _serviceService.Create(catalog);
        }

        [HttpPut("service")]
        public async Task UpdateService(CatalogFormDto catalog)
        {
            catalog.UsuarioId = "userId";
            await _serviceService.Update(catalog);
        }
    }
}
