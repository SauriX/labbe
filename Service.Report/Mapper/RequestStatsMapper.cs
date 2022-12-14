using Service.Report.Domain.MedicalRecord;
using Service.Report.Domain.Request;
using Service.Report.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Report.Mapper
{
    public static class RequestStatsMapper
    {
        public static IEnumerable<RequestStatsDto> ToRequestStatsDto(this IEnumerable<RequestInfo> model)
        {
            if (model == null) return null;

            return from c in model
                   group c by new { c.NombreCompleto, c.Expediente } into g
                   select new RequestStatsDto
                   {
                       Id = Guid.NewGuid(),
                       NoSolicitudes = g.Count(),
                       Paciente = g.Key.NombreCompleto,
                       Expediente = g.Key.Expediente
                   };
        }
    }
}
