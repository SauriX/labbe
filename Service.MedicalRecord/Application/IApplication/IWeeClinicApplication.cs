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
        Task<Laboratorio_BusquedaFolios_0> SearchPatientByFolio(string folio);
        Task<List<Laboratorio_BusquedaFolioLaboratorio_0>> GetServicesByFolio(string folio);
        Task<Laboratorio_GetPreciosEstudios_ByidServicio> GetServicePrice(string serviceId, string branch);
        Task<Laboratorio_GetPreciosEstudios_ByidServicio_0> ReleaseService(string serviceId);
    }
}
