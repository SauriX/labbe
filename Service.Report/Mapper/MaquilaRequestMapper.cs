using MoreLinq;
using Service.Report.Context;
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
        public static List<MaquilaRequestDto> ToMaquilaInternDto(this IEnumerable<RequestStudy> studies)
        {
            if (studies == null) return null;

            var results = MaquilaGeneric(studies.Where(x => x.Sucursal != null).Where(x => x.EstatusId >= 4 && x.EstatusId <= 8));

            return results;
        }

        public static List<MaquilaRequestDto> ToMaquilaExternDto(this IEnumerable<RequestStudy> studies)
        {
            if (studies == null) return null;

            var results = MaquilaGeneric(studies.Where(x => x.Maquila != null).Where(x => x.EstatusId >= 4 && x.EstatusId <= 8));

            return results;
        }

        public static IEnumerable<MaquilaRequestChartDto> ToMaquilaInternChartDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var studies = model.SelectMany(x => x.Estudios);

            var results = studies
                .Where(x => x.Sucursal != null)
                .Where(x => x.EstatusId >= 4 && x.EstatusId <= 8)
                .GroupBy(x => new { x.SucursalId, x.Sucursal?.Sucursal })
                .Select(g => new MaquilaRequestChartDto
            {
                Id = Guid.NewGuid(),
                Maquila = g.Key.Sucursal,
                NoSolicitudes = g.Select(x => x.SolicitudId).Distinct().Count(),
            });

            return results;
        }

        public static IEnumerable<MaquilaRequestChartDto> ToMaquilaExternalChartDto(this IEnumerable<RequestStudy> studies)
        {
            if (studies == null) return null;

            var results = studies
                .Where(x => x.Maquila != null)
                .Where(x => x.EstatusId >= 4 && x.EstatusId <= 8)
                .GroupBy(x => new { x.MaquilaId, x.Maquila?.Nombre })
                .Select(g => new MaquilaRequestChartDto
            {
                Id = Guid.NewGuid(),
                Maquila = g.Key.Nombre,
                NoSolicitudes = g.Select(x => x.SolicitudId).Distinct().Count(),
            });

            return results;
        }

        private static List<MaquilaRequestDto> MaquilaGeneric(IEnumerable<RequestStudy> studies)
        {
            return studies.Select(grupo =>
            {
                var request = grupo.Solicitud;
                var dueDate = studies.Max(x => x.Duracion);

                return new MaquilaRequestDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Clave,
                    Paciente = request.Expediente.Nombre,
                    Edad = request.Expediente.Edad,
                    Sexo = request.Expediente.Sexo,
                    Medico = request.Medico.NombreMedico,
                    FechaEntrega = request.Fecha.AddDays(dueDate).ToString("dd/MM/yyyy"),
                    ClaveEstudio = grupo.Clave,
                    NombreEstudio = grupo.Estudio,
                    Estatus = grupo.Estatus.Estatus,
                    Maquila = grupo.Maquila?.Nombre,
                    Sucursal = grupo.Sucursal?.Sucursal,
                };
            }).ToList();
        }
    }
}
