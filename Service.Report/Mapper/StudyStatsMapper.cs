using Automatonymous;
using Service.Report.Dictionary;
using Service.Report.Domain.Request;
using Service.Report.Dtos.StudyStats;
using Shared.Extensions;
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

            var results = (from c in model
                           group c by c.SolicitudId into grupo
                           select grupo)
                           .Select(grupo =>
                           {

                               var studies = grupo.SelectMany(x => x.Estudios);

                               return new StudyStatsChartDto
                               {
                                   CantidadPendiente = studies.Count(x => x.Estatus.Id == 1),
                                   CantidadTomaDeMuestra = studies.Count(x => x.Estatus.Id == 2),
                                   CantidadSolicitado = studies.Count(x => x.Estatus.Id == 3),
                                   CantidadCapturado = studies.Count(x => x.Estatus.Id == 4),
                                   CantidadValidado = studies.Count(x => x.Estatus.Id == 5),
                                   CantidadEnRuta = studies.Count(x => x.Estatus.Id == 6),
                                   CantidadLiberado = studies.Count(x => x.Estatus.Id == 7),
                                   CantidadEnviado = studies.Count(x => x.Estatus.Id == 8),
                                   CantidadEntregado = studies.Count(x => x.Estatus.Id == 9),
                                   CantidadCancelado = studies.Count(x => x.Estatus.Id == 10),
                               };
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
    }
}
