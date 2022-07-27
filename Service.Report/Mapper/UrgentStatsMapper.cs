using Service.Report.Domain.Request;
using Service.Report.Dtos.UrgentStats;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Report.Mapper
{
    public static class UrgentStatsMapper
    {
        public static IEnumerable<UrgentStatsDto> ToUrgentStatsDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = (from c in model
                           group c by new { c.SolicitudId, c.Clave, c.Expediente.Nombre, c.Medico.NombreMedico, c.Fecha, c.Expediente } into grupo
                           select grupo)
                           .Select(grupo =>
                           {
                               var studies = grupo.SelectMany(x => x.Estudios);
                               var dueDate = studies.Max(x => x.Duracion);

                               return new UrgentStatsDto
                               {
                                   Id = Guid.NewGuid(),
                                   Solicitud = grupo.Key.Clave,
                                   Paciente = grupo.Key.Nombre,
                                   Edad = grupo.Key.Expediente.Edad,
                                   Sexo = grupo.Key.Expediente.Sexo,
                                   Estudio = studies.ToStudiesDto(),
                                   Medico = grupo.Key.NombreMedico,
                                   FechaEntrega = grupo.Key.Fecha.AddDays(dueDate),
                               };
                           });

            return results;
        }
    }
}
