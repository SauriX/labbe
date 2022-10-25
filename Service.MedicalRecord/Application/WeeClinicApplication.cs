using Integration.WeeClinic.Models.Laboratorio_BusquedaFolioLaboratorio;
using Integration.WeeClinic.Models.Laboratorio_BusquedaFolios;
using Integration.WeeClinic.Services;
using Service.MedicalRecord.Application.IApplication;
using Shared.Error;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application
{
    public class WeeClinicApplication : IWeeClinicApplication
    {
        public async Task<Laboratorio_BusquedaFolios_0> SearchPatientByFolio(string folio)
        {
            var response = await LaboratoryService.BusquedaFolios(folio);

            if (response.Datos == null || response.Datos.Count == 0)
            {
                throw new CustomException(HttpStatusCode.NotFound, "No se encontró el servicio con el número de folio proporcionado");
            }

            return response.Datos[0];
        }

        public async Task<List<Laboratorio_BusquedaFolioLaboratorio_0>> GetServicesByFolio(string folio)
        {
            var response = await LaboratoryService.BuscaFolioLaboratorio(folio);

            if (response.Datos1 == null || response.Datos1.Count == 0)
            {
                throw new CustomException(HttpStatusCode.NotFound, "No se encontrarón datos para validar vigencia del servicio");
            }

            var validationData = response.Datos1[0];

            //if (validationData.CodEstatus == 1 || validationData.IsVigente == 1)
            //{
            //    throw new CustomException(HttpStatusCode.BadRequest, "El folio no se encuentra vigente");
            //}

            var services = response.Datos;

            if (services == null || services.Count == 0)
            {
                throw new CustomException(HttpStatusCode.NotFound, "No se encontrarón estudios");
            }

            return services;
        }
    }
}
