using Integration.WeeClinic.Models.Laboratorio_BusquedaFolioLaboratorio;
using Integration.WeeClinic.Models.Laboratorio_BusquedaFolios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IWeeClinicApplication
    {
        Task<Laboratorio_BusquedaFolios_0> SearchPatientByFolio(string folio);
        Task<List<Laboratorio_BusquedaFolioLaboratorio_0>> GetServicesByFolio(string folio);
    }
}
