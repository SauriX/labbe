using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dto.Series;
using Service.Catalog.Dtos.Series;
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
        private readonly ISeriesApplication _seriesApplication;

        public SeriesController(ISeriesApplication series)
        {
            _seriesApplication = series;
        }

        [HttpGet("branch/{branchId}/{type}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<SeriesListDto>> GetByBranch(Guid branchId, byte type)
        {
            var series = await _seriesApplication.GetByBranch(branchId, type);

            return series;
        }

        [HttpPost("filter")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<SeriesListDto>> GetByFilter(SeriesFilterDto filter)
        {
            var series = await _seriesApplication.GetByFilter(filter);

            return series;
        }

        [HttpPost("new")]
        [Authorize(Policies.Access)]
        public async Task<SeriesDto> GetByNewForm(SeriesNewDto newSerie)
        {
            newSerie.UsuarioId = (Guid)HttpContext.Items["userId"];
            var serie = await _seriesApplication.GetByNewForm(newSerie);

            return serie;
        }

        [HttpGet("{id}/{tipoSerie}")]
        [Authorize(Policies.Access)]
        public async Task<SeriesDto> GetById(int id, byte tipoSerie)
        {
            var serie = await _seriesApplication.GetById(id, tipoSerie);

            return serie;
        }
        
        [HttpGet("{branchId}")]
        [Authorize(Policies.Access)]
        public async Task<ExpeditionPlaceDto> GetBranch(string branchId)
        {
            var serie = await _seriesApplication.GetBranch(branchId);

            return serie;
        }

        [HttpPost("invoice")]
        [Authorize(Policies.Access)]
        public async Task CreateInvoice([FromForm] SeriesDto serie)
        {
            serie.UsuarioId = (Guid)HttpContext.Items["userId"];
            await _seriesApplication.CreateInvoice(serie);
        }

        [HttpPut("invoice")]
        [Authorize(Policies.Access)]
        public async Task UpdateInvoice([FromForm] SeriesDto serie)
        {
            await _seriesApplication.UpdateInvoice(serie);
        }
        
        [HttpPost("ticket")]
        [Authorize(Policies.Access)]
        public async Task CreateTicket(TicketDto ticket)
        {
            ticket.UsuarioId = (Guid)HttpContext.Items["userId"];
            await _seriesApplication.CreateTicket(ticket);
        }

        [HttpPut("ticket")]
        [Authorize(Policies.Access)]
        public async Task UpdateTicket(TicketDto ticket)
        {
            await _seriesApplication.UpdateTicket(ticket);
        }

        [HttpPost("export/list")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportSeriesList(SeriesFilterDto search)
        {
            var (file, fileName) = await _seriesApplication.ExportSeriesList(search);
            return File(file, MimeType.XLSX, fileName);
        }
    }
}
