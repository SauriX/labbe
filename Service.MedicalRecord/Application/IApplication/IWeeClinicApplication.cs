using Integration.WeeClinic.Models.Laboratorio_BusquedaFolios;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IWeeClinicApplication
    {
        Task<Laboratorio_BusquedaFolios_0> SearchByFolio(string folio);
    }
}
