using Integration.WeeClinic.Dtos;
using Integration.WeeClinic.Mapper;
using Integration.WeeClinic.Models.Laboratorio_BusquedaFolioLaboratorio;
using Integration.WeeClinic.Models.Laboratorio_BusquedaFolios;
using Integration.WeeClinic.Models.Laboratorio_GetPreciosEstudios_ByidServicio;
using Integration.WeeClinic.Services;
using Newtonsoft.Json;
using Service.MedicalRecord.Application.IApplication;
using Shared.Error;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application
{
    public class WeeClinicApplication : IWeeClinicApplication
    {
        private const string COD_ESTATUS_SELECCIONAR_ESTUDIO = "14";
        private const string COD_ESTATUS_LIBERAR_ESTUDIO = "1";

        public async Task<WeePatientInfoDto> SearchPatientByFolio(string folio)
        {
            var response = await LaboratoryService.BusquedaFolios(folio);

            if (response.Datos == null || response.Datos.Count == 0)
            {
                throw new CustomException(HttpStatusCode.NotFound, "No se encontró el servicio con el número de folio proporcionado");
            }

            var data = response.Datos[0].ToWeePatientInfoDto();

            return data;
        }

        public async Task<List<WeeServiceDto>> GetServicesByFolio(string folio)
        {
            var response = await LaboratoryService.BuscaFolioLaboratorio(folio);

            if (response.Datos1 == null || response.Datos1.Count == 0)
            {
                throw new CustomException(HttpStatusCode.NotFound, "No se encontrarón datos para validar vigencia del servicio");
            }

            var validationData = response.Datos1[0];

            //if (validationData.IsVigente == 1)
            //{
            //    throw new CustomException(HttpStatusCode.BadRequest, "El folio no se encuentra vigente");
            //}

            var services = response.Datos;

            if (services == null || services.Count == 0)
            {
                throw new CustomException(HttpStatusCode.NotFound, "No se encontrarón estudios");
            }

            var data = response.Datos.ToWeeServiceDto();

            return data;
        }

        public async Task<WeeServicePricesDto> GetServicePrice(string serviceId, string branch)
        {
            var response = await LaboratoryService.GetPreciosEstudios_ByidServicio(serviceId, branch, COD_ESTATUS_SELECCIONAR_ESTUDIO);

            if (response.Datos == null || response.Datos.Count == 0 || response.Datos1 == null || response.Datos1.Count == 0 || response.Datos2 == null || response.Datos2.Count == 0)
            {
                throw new CustomException(HttpStatusCode.NotFound, "Lista de precios WeeClinic incompleta");
            }

            if (response.Datos[0].PrecioUnitario == 0)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "El precio asignado del servicio es 0");
            }

            var data = response.ToWeeServicePricesDto();

            return data;
        }

        public async Task<Laboratorio_GetPreciosEstudios_ByidServicio_0> ReleaseService(string serviceId)
        {
            var response = await LaboratoryService.GetPreciosEstudios_ByidServicio(serviceId, null, COD_ESTATUS_LIBERAR_ESTUDIO);

            return response.Datos[0];
        }
    }
}