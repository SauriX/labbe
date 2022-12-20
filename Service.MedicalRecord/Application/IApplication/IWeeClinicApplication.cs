using Integration.WeeClinic.Dtos;
using Integration.WeeClinic.Models.Laboratorio_BusquedaFolioLaboratorio;
using Integration.WeeClinic.Models.Laboratorio_BusquedaFolios;
using Integration.WeeClinic.Models.Laboratorio_GetPreciosEstudios_ByidServicio;
using Microsoft.AspNetCore.Http;
using Service.MedicalRecord.Dtos.WeeClinic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IWeeClinicApplication
    {
        Task<WeePatientInfoDto> SearchPatientByFolio(string folio);
        Task<List<WeeServiceDto>> GetServicesByFolio(string folio);
        Task<WeeServicePricesDto> GetServicePrice(string serviceId, string branch);
        Task<WeeTokenValidationDto> OperateToken(string personId, string actionCode, string code = null);
        Task<WeeTokenVerificationDto> VerifyToken(string personId, string orderId, string code, string branch);
        Task<List<WeeServiceAssignmentDto>> AssignServices(List<WeeServiceNodeDto> services, string branch);
        Task<WeeCancellationDto> CancelService(string serviceId, string nodeId, string branch);
        Task<WeeUploadFileDto> UploadResultFile(IFormFile file);
        Task<WeeUploadFileDto> RelateResultFile(string idServicio, string idNodo, string idArchivo, string nota, int isRemplazarOrnew);
    }
}
