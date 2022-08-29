using Integration.WeeClinic;
using Integration.WeeClinic.Responses;
using Integration.WeeClinic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class WeeController : ControllerBase
    {
        [HttpGet("login")]
        public async Task<LoginResponse> Login()
        {
            return await Base.Login();
        }    
        
        [HttpGet("folio")]
        public async Task<string> BusquedaFolios()
        {
            return await LaboratoryService.BusquedaFolios();
        }     
        
        [HttpGet("estudio")]
        public async Task<string> BuscaFolioLaboratorio()
        {
            return await LaboratoryService.BuscaFolioLaboratorio();
        }   
        
        [HttpGet("precios")]
        public async Task<string> GetPreciosEstudios_ByidServicio()
        {
            return await LaboratoryService.GetPreciosEstudios_ByidServicio();
        }
    }
}
