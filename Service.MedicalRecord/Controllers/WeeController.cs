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
        
        [HttpGet("servicio1")]
        public async Task<string> BusquedaFolios()
        {
            return await LaboratoryService.BusquedaFolios();
        }     
        
        [HttpGet("servicio2")]
        public async Task<string> BuscaFolioLaboratorio()
        {
            return await LaboratoryService.BuscaFolioLaboratorio();
        }   
        
        [HttpGet("servicio3")]
        public async Task<string> GetPreciosEstudios_ByidServicio()
        {
            return await LaboratoryService.GetPreciosEstudios_ByidServicio();
        }      
        
        [HttpGet("servicio4")]
        public async Task<string> ValidarCodigoPacienteLaboratorio()
        {
            return await LaboratoryService.ValidarCodigoPacienteLaboratorio();
        }      
        
        [HttpGet("servicio5")]
        public async Task<string> Laboratorio_ValidaToken()
        {
            return await LaboratoryService.Laboratorio_ValidaToken();
        }     
        
        [HttpGet("servicio6")]
        public async Task<string> Laboratorio_AsignaEstudio()
        {
            return await LaboratoryService.Laboratorio_AsignaEstudio();
        }      
        
        [HttpGet("servicio7")]
        public async Task<string> Laboratorio_CancelaEstudios_ByProveedor()
        {
            return await LaboratoryService.Laboratorio_CancelaEstudios_ByProveedor();
        }      
    }
}
