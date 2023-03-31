using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Dtos.RportStudy;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.MedicalRecord.Mapper
{
    public static class ReportStudyMapper
    {
        public static List<ReportStudyListDto> ToReporteRequestStudyDto(this IEnumerable<Domain.Request.RequestStudy> studys)
        {

            return studys.Select(x => new ReportStudyListDto
            {
                IdStudio = x.EstudioId.ToString(),
                Clave = x.Clave,
                Nombre = x.Nombre,
                Estatus = x.Estatus.Nombre,
                Fecha = x.FechaEntrega.ToShortDateString(),
                Regitro = x.FechaCreo.ToShortDateString(),
                Color = x.Estatus.Color,

            }).ToList();
        }

        public static List<ReportRequestListDto> ToReportRequestList(this List<Domain.Request.Request> request)
        {
            if (request == null) return null;


            return request.Select(x => new ReportRequestListDto
            {

                ExpedienteId = x.ExpedienteId.ToString(),
                SolicitudId = x.Id.ToString(),
                Solicitud = x.Clave,
                Paciente = x.Expediente.NombreCompleto,
                Edad = x.Expediente.Edad.ToString(),
                Sexo = x.Expediente.Genero,
                Sucursal = x.Sucursal.Nombre.ToString(),
                Medico = x.Medico.Nombre,
                Tipo = x.Urgencia == 1 ? "Normal" : "Urgente",
                Compañia = x.Compañia.Nombre,
                Entrega = x.Estudios.Count() > 1 ? $"{x.Estudios.Select(y => y.FechaEntrega).Min().ToShortDateString()}-{x.Estudios.Select(y => y.FechaEntrega).Max().ToShortDateString()}" : x.Estudios.Any() ? x.Estudios.First().FechaEntrega.ToShortDateString() : "",
                Estudios = x.Estudios.ToReporteRequestStudyDto(),
                Estatus = GetEstatus(x),
                isPatologia = x.Estudios.Any(y => y.DepartamentoId == Catalogs.Department.PATOLOGIA)
            }).ToList();
        }

        private static string GetEstatus(Domain.Request.Request x)
        {
            if (x.Estudios.Any(y => y.DepartamentoId == Catalogs.Department.PATOLOGIA))
            {
                return x.ClavePatologica;
            }

            if (x.Urgencia == 1)
            {
                return x.Estudios.Any(y => y.EstatusId == Status.RequestStudy.EnRuta) ? "Ruta" : "Normal";
            }

            return "Urgente";
        }
    }
}
