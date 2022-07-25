using Service.Report.Domain.Request;
using Service.Report.Dtos.Request;
using System.Collections.Generic;
using System.Linq;

namespace Service.Report.Mapper
{
    public static class RequestStatsMapper
    {
        public static IEnumerable<RequestStatsDto> ToRequestStatsDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            return from c in model
                   group c by new { c.Expediente.Nombre, c.Expediente.Expediente } into g
                   select new RequestStatsDto
                   {
                       NoSolicitudes = g.Count(),
                       Paciente = g.Key.Nombre,
                       Expediente = g.Key.Expediente
                   };
        }
    }
}
