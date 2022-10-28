using Integration.WeeClinic.Dtos;
using Integration.WeeClinic.Models.Laboratorio_BusquedaFolioLaboratorio;
using Integration.WeeClinic.Models.Laboratorio_BusquedaFolios;
using Integration.WeeClinic.Models.Laboratorio_GetPreciosEstudios_ByidServicio;
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
        Task<Laboratorio_GetPreciosEstudios_ByidServicio_0> ReleaseService(string serviceId);
    }
}
