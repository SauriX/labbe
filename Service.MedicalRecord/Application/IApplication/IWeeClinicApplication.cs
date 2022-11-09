using Integration.WeeClinic.Models.Laboratorio_BusquedaFolioLaboratorio;
using Integration.WeeClinic.Models.Laboratorio_BusquedaFolios;
using Integration.WeeClinic.Models.Laboratorio_GetPreciosEstudios_ByidServicio;
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
    }
}
