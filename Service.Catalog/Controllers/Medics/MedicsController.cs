using Identidad.Api.Infraestructure.Services.IServices;
using Identidad.Api.ViewModels.Medicos;
using Identidad.Api.ViewModels.Menu;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identidad.Api.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class CatalogoMedicosController : ControllerBase
    {
        private readonly IMedicsApplication _Services;

        public CatalogoMedicosController(IMedicsApplication services)
        {
            _Services = services;
        }

        [HttpGet("{Id}")]

        public async Task<MedicsFormDto> GetById (int Id) 
        {
            return await _Services.GetById(Id);
        }

        [HttpGet("Services/all/{search}")]
        public async Task<IEnumerable<MedicsFormDto>> GetAll(string search = null)
        {
            return await _Services.GetAll(search);
        }

        public async Task Create(Medics CatalogoMedicos)
        {
            CatalogoMedicos.Clave = "Clave";
            await _Services.Create(CatalogoMedicos);
        }

        public async Task Update(Medics CatalogoMedicos)
        {
            CatalogoMedicos.Clave = "Clave";
            await _Services.Update(CatalogoMedicos);
        }
    }
}
