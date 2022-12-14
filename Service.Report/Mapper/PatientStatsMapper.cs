using Service.Report.Domain.MedicalRecord;
using Service.Report.Domain.Request;
using Service.Report.Dtos.PatientStats;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Report.Mapper
{
    public static class PatientStatsMapper
    {
        public static IEnumerable<PatientStatsDto> ToPatientStatsDto(this IEnumerable<RequestInfo> model)
        {
            if (model == null) return null;

            var results = (from c in model
                           group c by new { c.NombreCompleto, c.ExpedienteId } into g
                           select new PatientStatsDto
                           {
                               Id = Guid.NewGuid(),
                               Paciente = g.Key.NombreCompleto,
                               NoSolicitudes = g.Count(),
                               Total = g.Sum(x => x.TotalEstudios),
                           }).ToList();

            results.Add(new PatientStatsDto
            {
                Id = Guid.NewGuid(),
                Paciente = "Total",
                NoSolicitudes = results.Sum(x => x.NoSolicitudes),
                Total = results.Sum(x => x.Total)
            });

            return results;
        }
    }
}
