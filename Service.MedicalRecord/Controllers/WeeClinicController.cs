﻿using Integration.WeeClinic;
using Integration.WeeClinic.Models.Laboratorio_BusquedaFolioLaboratorio;
using Integration.WeeClinic.Models.Laboratorio_BusquedaFolios;
using Integration.WeeClinic.Models.Laboratorio_CargaResultados;
using Integration.WeeClinic.Models.Laboratorio_GetPreciosEstudios_ByidServicio;
using Integration.WeeClinic.Models.Laboratorio_ValidarCodigoPacienteLaboratorio;
using Integration.WeeClinic.Models.Laboratorio_ValidaToken;
using Integration.WeeClinic.Responses;
using Integration.WeeClinic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.WeeClinic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class WeeClinicController : ControllerBase
    {
        private readonly IWeeClinicApplication _service;

        public WeeClinicController(IWeeClinicApplication service)
        {
            _service = service;
        }

        [HttpGet("login")]
        public async Task<LoginResponse> Login()
        {
            return await Base.Login();
        }

        // Laboratorio
        [HttpGet("search/folio/{folio}")]
        public async Task<WeePatientInfoDto> SearchPatientByFolio(string folio)
        {
            return await _service.SearchPatientByFolio(folio);
        }

        [HttpGet("Laboratorio_BusquedaFolioLaboratorio/{folio}")]
        public async Task<Laboratorio_BusquedaFolioLaboratorio> BuscaFolioLaboratorio(string folio)
        {
            return await LaboratoryService.BuscaFolioLaboratorio(folio);
        }

        [HttpGet("Laboratorio_GetPreciosEstudios_ByidServicio/{serviceId}")]
        public async Task<Laboratorio_GetPreciosEstudios_ByidServicio> GetPreciosEstudios_ByidServicio(string serviceId, string branch, string status)
        {
            return await LaboratoryService.GetPreciosEstudios_ByidServicio(serviceId, branch, "14");
        }

        [HttpGet("Laboratorio_ValidarCodigoPacienteLaboratorio/{personId}/{actionCode}/{code}")]
        public async Task<Laboratorio_ValidarCodigoPacienteLaboratorio> ValidarCodigoPacienteLaboratorio(string personId, string actionCode, string code)
        {
            return await LaboratoryService.ValidarCodigoPacienteLaboratorio(personId, actionCode, code);
        }

        [HttpGet("Laboratorio_ValidaToken/{personId}/{orderId}/{code}")]
        public async Task<Laboratorio_ValidaToken> Laboratorio_ValidaToken(string personId, string orderId, string code)
        {
            return await LaboratoryService.Laboratorio_ValidaToken(personId, orderId, code, null);
        }

        //[HttpGet("servicio6")]
        //public async Task<string> Laboratorio_AsignaEstudio()
        //{
        //    return await LaboratoryService.Laboratorio_AsignaEstudio();
        //}

        //[HttpGet("servicio7")]
        //public async Task<string> Laboratorio_CancelaEstudios_ByProveedor()
        //{
        //    return await LaboratoryService.Laboratorio_CancelaEstudios_ByProveedor();
        //}

        [HttpPost("servicio8")]
        public async Task<Laboratorio_CargaResultados> UploadFileAzure(IFormFile file)
        {
            return await LaboratoryService.UploadFileAzure(file);
        }

        [HttpGet("servicio8-3")]
        public async Task<string> Laboratorio_CargaURL_Resultados()
        {
            return await LaboratoryService.Laboratorio_CargaURL_Resultados();
        }

        [HttpGet("servicio9")]
        public async Task<Laboratorio_RelacionResultados> Laboratorio_ArchivosResultados_Update(string idServicio, string idNodo, string idArchivo, string nota, int isRemplazarOrnew)
        {
            return await LaboratoryService.Laboratorio_ArchivosResultados_Update(idServicio, idNodo, idArchivo, nota, isRemplazarOrnew);
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
