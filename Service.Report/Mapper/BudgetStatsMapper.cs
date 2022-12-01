using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Service.Report.Domain.Request;
using Service.Report.Dtos;
using Service.Report.Dtos.BudgetStats;
using Service.Report.Dtos.TypeRequest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Service.Report.Mapper
{
    public static class BudgetStatsMapper
    {
        public static IEnumerable<BudgetStatsDto> ToBudgetRequestDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = BudgetStatsGeneric(model);

            return results;
        }

        public static List<BudgetStatsDto> BudgetStatsGeneric(IEnumerable<Request> model)
        {
            return model.Where(x => x.Estudios != null).Select(request =>
            {
                var studies = request.Estudios;
                var pack = request.Paquetes;

                var priceStudies = studies.Sum(x => x.Precio - (x.Precio * x.Paquete?.DescuentoPorcentaje ?? 0) - (x.Descuento == 0 ? 0 : x.Descuento));
                var descount = request.Descuento;
                var promotion = studies.Sum(x => x.Descuento) + pack.Sum(x => x.Descuento);
                var porcentualDescount = (descount * 100) / priceStudies;
                var descRequest = request.Descuento / 100;

                return new BudgetStatsDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Clave,
                    Sucursal = request.Sucursal.Sucursal,
                    NombrePaciente = request.Expediente.Nombre,
                    NombreMedico = request.Medico.NombreMedico,
                    Estudio = studies.PromotionStudies(),
                    Descuento = descount,
                    DescuentoPorcentual = porcentualDescount,
                    Promocion = promotion,
                };
            }).ToList();
        }

        public static IEnumerable<BudgetStatsChartDto> ToBudgetStatsChartDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = (from c in model.Where(x => x.Estudios != null)
                           group c by new { c.SucursalId, c.Sucursal.Sucursal, c.Fecha.Year, c.Fecha.Month } into grupo
                           select new BudgetStatsChartDto
                           {
                               Id = Guid.NewGuid(),
                               Sucursal = grupo.Key.Sucursal,
                               Fecha = new DateTime(grupo.Key.Year, grupo.Key.Month, 1).ToString("MM/yyyy"),
                               Total = grupo.Select(x => x.Estudios.Sum(x => x.PrecioFinal)).Sum(),
                           });
            return results;
        }

        public static BudgetDto ToBudgetDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = BudgetStatsGeneric(model);

            var totals = new InvoiceDto
            {
                SumaEstudios = results.Sum(x => x.PrecioEstudios),
                SumaDescuentos = results.Sum(x => x.Descuento),
            };

            var data = new BudgetDto
            {
                BudgetStats = results,
                BudgetTotal = totals
            };

            return data;
        }
    }
}
