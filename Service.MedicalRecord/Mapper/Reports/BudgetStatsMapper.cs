using System.Collections.Generic;
using Service.MedicalRecord.Domain.Request;
using System.Collections;
using System.Linq;
using System;
using Service.MedicalRecord.Dtos.Reports.BudgetStats;
using Service.MedicalRecord.Domain.Quotation;
using Service.MedicalRecord.Dtos.Reports.StudyStats;
using Service.MedicalRecord.Dtos.Reports;

namespace Service.MedicalRecord.Mapper.Reports
{
    public static class BudgetStatsMapper
    {
        public static IEnumerable<BudgetStatsDto> ToBudgetRequestDto(this IEnumerable<Quotation> model)
        {
            if (model == null) return null;

            var results = BudgetStatsGeneric(model);

            return results;
        }

        public static List<BudgetStatsDto> BudgetStatsGeneric(IEnumerable<Quotation> model)
        {
            return model.Where(x => x.Estudios != null).Select(request =>
            {
                var studies = request.Estudios;
                var pack = request.Paquetes;

                var priceStudies = studies.Sum(x => x.Precio - (x.Precio * x.Paquete?.DescuentoPorcentaje ?? 0) - (x.Descuento == 0 ? 0 : x.Descuento));
                var descount = studies.Sum(x => x.Descuento);
                var promotion = studies.Sum(x => x.Descuento) + pack.Sum(x => x.Descuento);
                var porcentualDescount = (descount * 100) / priceStudies;
                var descRequest = descount / 100;

                return new BudgetStatsDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Clave,
                    Sucursal = request.Sucursal.Nombre,
                    NombrePaciente = request.Expediente.NombreCompleto,
                    NombreMedico = request.Medico.Nombre,
                    Estudio = studies.QuotationStudies(),
                    Descuento = descount,
                    DescuentoPorcentual = porcentualDescount,
                    Promocion = promotion,
                };
            }).ToList();
        }

        public static IEnumerable<BudgetStatsChartDto> ToBudgetStatsChartDto(this IEnumerable<Quotation> model)
        {
            if (model == null) return null;

            var results = (from c in model.Where(x => x.Estudios != null)
                           group c by new { c.SucursalId, c.Sucursal.Nombre, c.FechaCreo.Year, c.FechaCreo.Month } into grupo
                           select new BudgetStatsChartDto
                           {
                               Id = Guid.NewGuid(),
                               Sucursal = grupo.Key.Nombre,
                               Fecha = new DateTime(grupo.Key.Year, grupo.Key.Month, 1).ToString("MM/yyyy"),
                               Total = grupo.Select(x => x.Estudios.Sum(x => x.PrecioFinal)).Sum(),
                           });
            return results;
        }

        public static BudgetDto ToBudgetDto(this IEnumerable<Quotation> model)
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

        public static List<StudiesDto> QuotationStudies(this IEnumerable<QuotationStudy> studies)
        {
            return studies.Select(x => new StudiesDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Precio = x.Precio,
                Descuento = x.Descuento,
                Paquete = x.Paquete?.Nombre,
                Promocion = x.Paquete?.Descuento / studies.Count(),
                PrecioFinal = x.Precio - (x.Precio * x.Paquete?.DescuentoPorcentaje ?? 0) - (x.Descuento == 0 ? 0 : x.Descuento),
            }).ToList();
        }
    }
}
