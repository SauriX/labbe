using Service.MedicalRecord.Dtos;
using System.Collections.Generic;
using System.Linq;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Sampling;
using Service.MedicalRecord.Dtos.ClinicResults;
using Service.MedicalRecord.Domain;
using System;
using Service.MedicalRecord.Dtos.Request;

namespace Service.MedicalRecord.Mapper
{
    public static class ClinicResultsMapper
    {
        public static List<ClinicResultsDto> ToClinicResultsDto(this List<Request> model)
        {
            if (model == null) return null;

            return model.Select(x => new ClinicResultsDto
            {
                Solicitud = x.Clave,
                Nombre = x.Expediente.NombreCompleto,
                Registro = x.FechaCreo.ToString("G"),
                Sucursal = x.Sucursal.Clave,
                Edad = x.Expediente.Edad.ToString(),
                Sexo = x.Expediente.Genero,
                Compañia = x.Compañia?.Nombre,
                Estudios = x.Estudios.ToClinics(),
                Id = x.Id.ToString(),
                Procedencia = x.Procedencia,
                SucursalNombre = x.Sucursal.Nombre,
                NombreMedico = x.Medico.Nombre,
                UsuarioCreo = x.UsuarioCreo
            }).ToList();
        }

        public static List<StudyDto> ToClinics(this ICollection<RequestStudy> model)
        {
            return model.Select(x => new StudyDto
            {
                EstudioId = x.EstudioId,
                Nombre = x.Nombre,
                Area = "",
                EstatusId = x.EstatusId,
                Registro = x.FechaCreo.ToString("G"),
                Entrega = x.FechaCreo.AddDays((double)x.Dias).ToString("G"),
                Seleccion = false,
                Clave = x.Clave,
                NombreEstatus = x.Estatus.Nombre,
            }).ToList();
        }

        public static List<ClinicResults> ToCaptureResults(this List<ClinicResultsCaptureDto> model)
        {
            return model.Select(x => new ClinicResults
            {
                Id = x.Id,
                Nombre = x.Nombre,
                SolicitudId = x.SolicitudId,
                EstudioId = x.EstudioId,
                TipoValorId = x.TipoValor,
                ValorInicial = x.ValorInicial,
                ValorFinal = x.ValorFinal,
                ParametroId = Guid.Parse(x.ParametroId),
                Resultado = x.Resultado
            }).ToList();
        }

        public static ClinicResultsPdfDto ToResults(this IEnumerable<ClinicResults> model)
        {
            if (model == null || !model.Any()) return new ClinicResultsPdfDto();

            var results = ResultsGeneric(model);
            var requestInfo = model.First();

            var request = new Dtos.ClinicResults.ClinicResultsRequestDto
            {
                Id = (Guid)(requestInfo.Solicitud.Id),
                Clave = requestInfo.Solicitud.Clave,
                Paciente = requestInfo.Solicitud.Expediente.NombrePaciente,
                Medico = requestInfo.Solicitud.Medico.Nombre,
                Compañia = requestInfo.Solicitud.Compañia.Nombre,
                Expediente = requestInfo.Solicitud.Expediente.Expediente,
                Edad = requestInfo.Solicitud.Expediente.Edad,
                Sexo = requestInfo.Solicitud.Expediente.Genero,
                FechaAdmision = requestInfo.Solicitud.FechaCreo.ToString("d"),
            };

            var data = new ClinicResultsPdfDto
            {
                CapturaResultados = results,
                SolicitudInfo = request,
            };

            return data;
        }

        public static List<ClinicResultsCaptureDto> ResultsGeneric(this IEnumerable<ClinicResults> model)
        {
            return model.Select(results =>
            {
                return new ClinicResultsCaptureDto
                {
                    Nombre = results.NombreParametro,
                    TipoValor = results.TipoValorId,
                    ValorInicial = results.ValorInicial,
                    ValorFinal = results.ValorInicial,
                    ParametroId = results.ParametroId.ToString(),
                    Resultado = results.Resultado,
                };
            }).ToList();
        }
    }
}
