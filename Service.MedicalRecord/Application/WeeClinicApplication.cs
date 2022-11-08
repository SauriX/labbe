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
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
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

            var services = await GetServicesByFolio(folio);

            var data = response.Datos[0].ToWeePatientInfoDto();

            var tempSt = services.First();
            data.FechaNacimiento = string.IsNullOrEmpty(tempSt.FechaNacimiento) ? null : DateTime.ParseExact(tempSt.FechaNacimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            data.Correo = tempSt.Correo_Paciente;
            data.Telefono = string.IsNullOrEmpty(tempSt.Telefono_Paciente) ? null :
              tempSt.Telefono_Paciente.Length != 10 ? tempSt.Telefono_Paciente :
              Regex.Replace(tempSt.Telefono_Paciente, @"^(...)(...)(..)(..)$", "$1-$2-$3-$4");

            data.Estudios = services.Select(x => new WeePatientInfoStudyDto
            {
                IdServicio = x.IdServicio,
                IdNodo = x.IdNodo,
                Clave = x.ClaveCDP,
                Nombre = x.CDPNombre,
                DescripcionWeeClinic = x.DescripcionInterna,
                Cantidad = Convert.ToInt32(x.CantidadSolicitada)
            });

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

        public async Task ValidateToken(string folio, string token)
        {
            //var response = await LaboratoryService.ValidarCodigoPacienteLaboratorio();
        }
    }
}