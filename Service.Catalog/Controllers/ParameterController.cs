using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Parameters;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParameterController : ControllerBase
    {
        private readonly IParameterApplication _ParameterService;

        public ParameterController(IParameterApplication indicationService)
        {
            _ParameterService = indicationService;
        }

        [HttpPost]
        public async Task Create(ParameterForm Parameter)
        {
            await _ParameterService.Create(Parameter);
        }

        [HttpPut]
        public async Task Update(ParameterForm parameter)
        {
            await _ParameterService.Update(parameter);
        }

        [HttpGet("all/{search?}")]
        public async Task<IEnumerable<ParameterList>> GetAll(string search = null)
        {

            return await _ParameterService.GetAll(search);
        }
        [HttpGet("all/values/{id?}/{tipe?}")]
        public async Task<IEnumerable<ValorTipeForm>> Values(string id,string tipe)
        {

            return await _ParameterService.getallvalues(id,tipe);
        }
        [HttpGet("{id}")]
        public async Task<ParameterForm> GetById(string id) {
            return await _ParameterService.GetById(id);
        }

        [HttpPost("addValue")]
        public async Task AddValue(ValorTipeForm valorTipeForm) { 
            await _ParameterService.AddValue(valorTipeForm);
        }

        [HttpGet("valuetipe/{id}")]
        public async Task<ValorTipeForm> GetValor(string id) {
            return await _ParameterService.getvalueNum(id);
        }

        [HttpPut("valuetipe")]
        public async Task Update(ValorTipeForm tipeForm)
        {
            await _ParameterService.updateValueNumeric(tipeForm);
        }

        [HttpDelete("{id?}")]
        public async Task deletevalue(string id) {
            await _ParameterService.deletevalue(id);        
        }

        [HttpPost("export/list/{search?}")]
        public async Task<IActionResult> ExportList(string search = null)
        {
            var file = await _ParameterService.ExportList(search);
            return File(file, MimeType.XLSX);
        }

        [HttpPost("export/form/{id}")]
        public async Task<IActionResult> ExportForm(string id)
        {
            var file = await _ParameterService.ExportForm(id);
            return File(file, MimeType.XLSX);
        }
    }
}
