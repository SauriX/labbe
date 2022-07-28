using Service.Report.Dictionary;
using Service.Report.Domain.Request;
using Service.Report.Dtos.StudyStats;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Report.Mapper
{
    public static class StudyStatsMapper
    {
        public static IEnumerable<StudyStatsDto> ToStudyStatsDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = (from c in model
                           group c by new { c.SolicitudId, c.Clave, c.Expediente.Nombre, c.Medico.NombreMedico, c.Fecha, c.Expediente, c.Parcialidad } into grupo
                           select grupo)
                           .Select(grupo =>
                           {
                               var studies = grupo.SelectMany(x => x.Estudios);
                               var dueDate = studies.Max(x => x.Duracion);

                               return new StudyStatsDto
                               {
                                   Id = Guid.NewGuid(),
                                   Solicitud = grupo.Key.Clave,
                                   Paciente = grupo.Key.Nombre,
                                   Edad = grupo.Key.Expediente.Edad,
                                   Sexo = grupo.Key.Expediente.Sexo,
                                   Estudio = studies.ToStudiesDto(),
                                   Medico = grupo.Key.NombreMedico,
                                   FechaEntrega = grupo.Key.Fecha.AddDays(dueDate),
                                   Fecha = grupo.Key.Fecha,
                                   Parcialidad = grupo.Key.Parcialidad,
                               };
                           });

            return results;
        }

        public static IEnumerable<StudyStatsChartDto> ToStudyStatsChartDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;
            var studies = model.SelectMany(x => x.Estudios);

            var results = (from c in studies
                           group c by new { c.Estatus.Estatus, c.EstatusId } into grupo
                           select new StudyStatsChartDto
                           {
                               Id = Guid.NewGuid(),
                               Estatus = grupo.Key.Estatus,
                               Cantidad = grupo.Count(),
                               Color = GetColor(grupo.Key.EstatusId),
                           });


            return results;
        }

        public static List<StudiesDto> ToStudiesDto(this IEnumerable<RequestStudy> studies)
        {
            return studies.Select(x => new StudiesDto
            {
                Clave = x.Clave,
                Estudio = x.Estudio,
                Estatus = x.Estatus.Estatus
            }).ToList();
        }

        public static string GetColor(byte statusId)
        {
            if (statusId == Status.Request.Pendiente) return "#536FC6";
            else if (statusId == Status.Request.TomaDeMuestra) return "#91CC75";
            else if (statusId == Status.Request.Solicitado) return "#F9C857";
            else if (statusId == Status.Request.Capturado) return "#71BFDD";
            else if (statusId == Status.Request.Validado) return "#3AA271";
            else if (statusId == Status.Request.EnRuta) return "#FB8350";
            else if (statusId == Status.Request.Liberado) return "#9960B3";
            else if (statusId == Status.Request.Enviado) return "#EA7BCC";
            else if (statusId == Status.Request.Entregado) return "#E7EFC5";
            else if (statusId == Status.Request.Cancelado) return "#EE6665";

            return "#2f54eb";
        }
    }
}
