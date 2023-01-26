using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Billing.Application.IApplication;
using Service.Billing.Dto.Series;
using Service.Billing.Dtos.Series;
using Shared.Dictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesApplication _service;

        public SeriesController(ISeriesApplication service)
        {
            _service = service;
        }

        [HttpGet("branch/{branchId}/{type}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<SeriesListDto>> GetByBranch(Guid branchId, byte type)
        {
            var series = await _service.GetByBranch(branchId, type);

            return series;
        }

        [HttpPost("filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<SeriesListDto>> GetByFilter(SeriesFilterDto filter)
        {
            var series = await _service.GetByFilter(filter);

            return series;
        }

        [HttpPost("new")]
        [Authorize(Policies.Access)]
        public async Task<SeriesDto> GetByNewForm(SeriesNewDto newSerie)
        {
            newSerie.UsuarioId = (Guid)HttpContext.Items["userId"];
            var serie = await _service.GetByNewForm(newSerie);

            return serie;
        }

        [HttpPost("{id}")]
        [Authorize(Policies.Access)]
        public async Task<SeriesDto> GetById(int id)
        {
            var serie = await _service.GetById(id);

            return serie;
        }

        [HttpPost("invoice")]
        [Authorize(Policies.Access)]
        public async Task CreateInvoice(SeriesDto serie)
        {
            serie.UsuarioId = (Guid)HttpContext.Items["userId"];
            await _service.CreateInvoice(serie);
        }

        [HttpPut("invoice")]
        [Authorize(Policies.Access)]
        public async Task UpdateInvoice(SeriesDto serie)
        {
            await _service.UpdateInvoice(serie);
        }
        
        [HttpPost("ticket")]
        [Authorize(Policies.Access)]
        public async Task CreateTicket(TicketDto ticket)
        {
            ticket.UsuarioId = (Guid)HttpContext.Items["userId"];
            await _service.UpdateTicket(ticket);
        }

        [HttpPut("ticket")]
        [Authorize(Policies.Access)]
        public async Task UpdateTicket(TicketDto ticket)
        {
            await _service.UpdateTicket(ticket);
        }

        [HttpPost("export/list")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportSeriesList(SeriesFilterDto search)
        {
            var (file, fileName) = await _service.ExportSeriesList(search);
            return File(file, MimeType.XLSX, fileName);
        }
    }
}
