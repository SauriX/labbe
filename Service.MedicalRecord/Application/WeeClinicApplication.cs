using Integration.WeeClinic.Models.Laboratorio_BusquedaFolios;
using Integration.WeeClinic.Services;
using Service.MedicalRecord.Application.IApplication;
using Shared.Error;
using System.Net;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application
{
    public class WeeClinicApplication : IWeeClinicApplication
    {
        public async Task<Laboratorio_BusquedaFolios_0> SearchByFolio(string folio)
        {
            var response = await LaboratoryService.BusquedaFolios(folio);

            if (response.Datos == null || response.Datos.Count == 0)
            {
                throw new CustomException(HttpStatusCode.NotFound, "No se encontró el servicio con el número de folio proporcionado");
            }

            return response.Datos[0];
        }
    }
}
