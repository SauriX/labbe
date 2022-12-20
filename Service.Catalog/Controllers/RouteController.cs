using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Route;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteApplication _service;

        public RouteController(IRouteApplication service)
        {
            _service = service;
        }

        [HttpGet("all/{search}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<RouteListDto>> GetAll(string search)
        {
            return await _service.GetAll(search);
        }

        [HttpGet("active")]
        public async Task<IEnumerable<RouteListDto>> GetActive()
        {
            return await _service.GetActive();
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<RouteFormDto> GetById(string id)
        {
            return await _service.GetById(id);
        }

        [HttpPost("multiple")]
        [Authorize(Policies.Access)]
        public async Task<List<RouteFormDto>> GetByIdmulty(List<string> id)
        {
            List<RouteFormDto> routes = new List<RouteFormDto>();
            foreach (var item in id) { 
                var route = await _service.GetById(item);
                routes.Add(route);
            }
            return routes;
        }
        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<RouteListDto> Create(RouteFormDto routes)
        {
            routes.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Create(routes);
        }

        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<RouteListDto> Update(RouteFormDto routes)
        {
            routes.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.Update(routes);
        }

        [HttpPost("export/list/{search}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportList(string search)
        {
            var (file, fileName) = await _service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportForm(string id)
        {
            var (file, fileName) = await _service.ExportForm(id);
            return File(file, MimeType.XLSX, fileName);
        }
        [HttpPost("find")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<RouteFormDto>> Find(RouteFormDto routes)
        {
            routes.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _service.FindRoutes(routes);
        }
    }
}
