﻿using Service.Report.Domain.MedicalRecord;
using Service.Report.Domain.Request;
using Service.Report.Dtos;
using Service.Report.Dtos.CanceledRequest;
using Service.Report.Dtos.CompanyStats;
using Service.Report.Dtos.StudyStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Mapper
{
    public static class CanceledRequestMapper
    {
        public static IEnumerable<CanceledRequestDto> ToCanceledRequestDto(this IEnumerable<RequestInfo> model)
        {
            if (model == null) return null;

            var results = CanceledGeneric(model);

            return results;
        }

        public static CanceledDto ToCanceledDto(this IEnumerable<RequestInfo> model)
        {
            if (model == null) return null;

            var results = CanceledGeneric(model);

            var totals = new InvoiceDto
            {
                SumaEstudios = results.Sum(x => x.PrecioEstudios),
                SumaDescuentos = results.Sum(x => x.Descuento),
            };

            var data = new CanceledDto
            {
                CanceledRequest = results,
                CanceledTotal = totals
            };

            return data;
        }

        public static IEnumerable<CanceledRequestChartDto> ToCanceledRequestChartDto(this IEnumerable<RequestInfo> model)
        {
            if (model == null) return null;

            var results = (from c in model.Where(x => x.EstatusId == 3)
                           group c by new { c.SucursalId, c.Sucursal } into grupo
                           select new CanceledRequestChartDto
                           {
                               Id = Guid.NewGuid(),
                               Sucursal = grupo.Key.Sucursal,
                               Cantidad = grupo.Count(),
                           });


            return results;
        }

        public static List<CanceledRequestDto> CanceledGeneric(IEnumerable<RequestInfo> model)
        {
            return model.Where(x => x.EstatusId == 3).Select(request =>
            {
                var studies = request.Estudios;

                return new CanceledRequestDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Solicitud,
                    Paciente = request.NombreCompleto,
                    Medico = request.Medico,
                    Empresa = request.Compañia,
                    Estudio = studies.GenericStudies(),
                    Descuento = request.Descuento,
                    DescuentoPorcentual = request.DescuentoPorcentual,
                };
            }).ToList();
        }
    }
}
