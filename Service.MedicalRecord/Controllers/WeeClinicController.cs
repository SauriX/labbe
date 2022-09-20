using Integration.WeeClinic;
using Integration.WeeClinic.Models.Laboratorio_BusquedaFolios;
using Integration.WeeClinic.Responses;
using Integration.WeeClinic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class WeeClinicController : ControllerBase
    {
        [HttpGet("login")]
        public async Task<LoginResponse> Login()
        {
            return await Base.Login();
        }

        // Laboratorio
        [HttpGet("Laboratorio_BusquedaFolios/{folio}")]
        public async Task<List<Laboratorio_BusquedaFolios>> BusquedaFolios(string folio)
        {
            return await LaboratoryService.BusquedaFolios(folio);
        }

        [HttpGet("BuscaFolioLaboratorio/{folio}")]
        public async Task<string> BuscaFolioLaboratorio(string folio)
        {
            return await LaboratoryService.BuscaFolioLaboratorio(folio);
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

        [HttpPost("servicio8")]
        public async Task<string> UploadFileAzure(IFormFile file)
        {
            return await LaboratoryService.UploadFileAzure(file);
        }

        [HttpGet("servicio8-3")]
        public async Task<string> Laboratorio_CargaURL_Resultados()
        {
            return await LaboratoryService.Laboratorio_CargaURL_Resultados();
        }

        [HttpGet("servicio9")]
        public async Task<string> Laboratorio_ArchivosResultados_Update()
        {
            return await LaboratoryService.Laboratorio_ArchivosResultados_Update();
        }

        [HttpGet("servicio10")]
        public async Task<string> Laboratorio_ArchivosResultados_Update_RemplazaArchivos()
        {
            return await LaboratoryService.Laboratorio_ArchivosResultados_Update_RemplazaArchivos();
        }

        [HttpGet("servicio11")]
        public async Task<string> SendEmailResulatdosLaboratorio()
        {
            return await LaboratoryService.SendEmailResulatdosLaboratorio();
        }

        [HttpGet("servicio18")]
        public async Task<string> Laboratorio_GetDetallePaquete()
        {
            return await LaboratoryService.Laboratorio_GetDetallePaquete();
        }

        [HttpGet("servicio19")]
        public async Task<string> SucursalAreas_GetLaboratoriosByUser()
        {
            return await LaboratoryService.SucursalAreas_GetLaboratoriosByUser();
        }

        [HttpGet("servicio20")]
        public async Task<string> Laboratorio_GetEstudiosbyLaboratorio()
        {
            return await LaboratoryService.Laboratorio_GetEstudiosbyLaboratorio();
        }

        [HttpGet("servicio21")]
        public async Task<string> Laboratorio_GetPreciosEstudiosPerfiles()
        {
            return await LaboratoryService.Laboratorio_GetPreciosEstudiosPerfiles();
        }

        [HttpGet("finservicio1")]
        public async Task<string> Factura_AddNewFactura()
        {
            return await FinancesService.Factura_AddNewFactura();
        }

        [HttpGet("finservicio2")]
        public async Task<string> Finanzas_GetClienteByFactura()
        {
            return await FinancesService.Finanzas_GetClienteByFactura();
        }

        [HttpGet("finservicio3")]
        public async Task<string> Facturas_GetSeparacionbyCliente()
        {
            return await FinancesService.Facturas_GetSeparacionbyCliente();
        }

        [HttpGet("finservicio4")]
        public async Task<string> Sucursal_GetSucursalByProveedor()
        {
            return await FinancesService.Sucursal_GetSucursalByProveedor();
        }

        [HttpGet("finservicio5")]
        public async Task<string> Finanzas_GetServiciosByFactura()
        {
            return await FinancesService.Finanzas_GetServiciosByFactura();
        }

        [HttpGet("finservicio6")]
        public async Task<string> Finanzas_AddFacturaServicios()
        {
            return await FinancesService.Finanzas_AddFacturaServicios();
        }

        [HttpGet("finservicio7")]
        public async Task<string> Finanzas_DeleteFacturaServicio()
        {
            return await FinancesService.Finanzas_DeleteFacturaServicio();
        }

        [HttpPost("finservicio8")]
        public async Task<string> Finanzas_UploadFileAzure(IFormFile file)
        {
            return await FinancesService.UploadFileAzure(file);
        }

        [HttpGet("finservicio9")]
        public async Task<string> Factura_AddFileFactura()
        {
            return await FinancesService.Factura_AddFileFactura();
        }
    }
}
