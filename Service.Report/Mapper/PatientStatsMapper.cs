using Service.Report.Domain.Request;
using Service.Report.Dtos.PatientStats;
using System.Collections.Generic;
using System.Linq;

namespace Service.Report.Mapper
{
    public static class PatientStatsMapper
    {
        public static IEnumerable<PatientStatsDto> ToPatientStatsDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = (from c in model
                           group c by new { c.Expediente.Nombre, c.ExpedienteId } into g
                           select new PatientStatsDto
                           {
                               Paciente = g.Key.Nombre,
                               NoSolicitudes = g.Count(),
                               Total = g.Sum(x => x.PrecioFinal),
                           }).ToList();

            results.Add(new PatientStatsDto
            {
                Paciente = "Total",
                NoSolicitudes = results.Sum(x => x.NoSolicitudes),
                Total = results.Sum(x => x.Total)
            });

            return results;
        }
    }
}
