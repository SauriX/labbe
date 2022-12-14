using MoreLinq;
using Service.Report.Context;
using Service.Report.Domain.MedicalRecord;
using Service.Report.Domain.Request;
using Service.Report.Dtos;
using Service.Report.Dtos.BondedRequest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Report.Mapper
{
    public static class MaquilaRequestMapper
    {
        public static List<MaquilaRequestDto> ToMaquilaInternDto(this IEnumerable<RequestStudies> studies)
        {
            if (studies == null) return null;

            var results = MaquilaGeneric(studies.Where(x => x.Solicitud.Sucursal != null).Where(x => x.EstatusId >= 4 && x.EstatusId <= 8));

            return results;
        }

        public static List<MaquilaRequestDto> ToMaquilaExternDto(this IEnumerable<RequestStudies> studies)
        {
            if (studies == null) return null;

            var results = MaquilaGeneric(studies.Where(x => x.Maquila != null).Where(x => x.EstatusId >= 4 && x.EstatusId <= 8));

            return results;
        }

        public static IEnumerable<MaquilaRequestChartDto> ToMaquilaInternChartDto(this IEnumerable<RequestInfo> model)
        {
            if (model == null) return null;

            var studies = model.SelectMany(x => x.Estudios);

            var results = studies
                .Where(x => x.Solicitud.Sucursal != null)
                .Where(x => x.EstatusId >= 4 && x.EstatusId <= 8)
                .GroupBy(x => new { x.Solicitud.SucursalId, x.Solicitud?.Sucursal })
                .Select(g => new MaquilaRequestChartDto
            {
                Id = Guid.NewGuid(),
                Maquila = g.Key.Sucursal,
                NoSolicitudes = g.Select(x => x.Solicitud.Id).Distinct().Count(),
            });

            return results;
        }

        public static IEnumerable<MaquilaRequestChartDto> ToMaquilaExternalChartDto(this IEnumerable<RequestStudies> studies)
        {
            if (studies == null) return null;

            var results = studies
                .Where(x => x.Maquila != null)
                .Where(x => x.EstatusId >= 4 && x.EstatusId <= 8)
                .GroupBy(x => new { x.MaquilaId, x.Maquila})
                .Select(g => new MaquilaRequestChartDto
            {
                Id = Guid.NewGuid(),
                Maquila = g.Key.Maquila,
                NoSolicitudes = g.Select(x => x.Solicitud.Id).Distinct().Count(),
            });

            return results;
        }

        private static List<MaquilaRequestDto> MaquilaGeneric(IEnumerable<RequestStudies> studies)
        {
            return studies.Select(grupo =>
            {
                var request = grupo.Solicitud;
                var dueDate = studies.Max(x => x.Dias);

                return new MaquilaRequestDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Solicitud,
                    Paciente = request.NombreCompleto,
                    Edad = request.Edad,
                    Sexo = request.Sexo,
                    Medico = request.Medico,
                    FechaEntrega = request.Fecha.AddDays((double)dueDate).ToString("dd/MM/yyyy"),
                    ClaveEstudio = grupo.Clave,
                    NombreEstudio = grupo.Nombre,
                    Estatus = grupo.Estatus,
                    Maquila = grupo.Nombre,
                    Sucursal = request.Sucursal,
                };
            }).ToList();
        }
    }
}
