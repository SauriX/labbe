using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Service.Report.Domain.MedicalRecord;
using Service.Report.Domain.Request;
using Service.Report.Dtos;
using Service.Report.Dtos.BudgetStats;
using Service.Report.Dtos.StudyStats;
using Service.Report.Dtos.TypeRequest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Service.Report.Mapper
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
            return model.Where(x => x.Estudio.Count > 0).Select(request =>
            {
                var studies = request.Estudio;

                return new BudgetStatsDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Solicitud,
                    Sucursal = request.Sucursal,
                    NombrePaciente = request.NombrePaciente,
                    NombreMedico = request.NombreMedico,
                    Estudio = studies.GenericStudies(),
                    Descuento = request.Descuento,
                    DescuentoPorcentual = request.DescuentoPorcentual,
                    Promocion = request.Promocion,
                };
            }).ToList();
        }

        public static IEnumerable<BudgetStatsChartDto> ToBudgetStatsChartDto(this IEnumerable<Quotation> model)
        {
            if (model == null) return null;

            var results = (from c in model.Where(x => x.Estudio.Count > 0)
                           group c by new { c.SucursalId, c.Sucursal, c.Fecha.Year, c.Fecha.Month } into grupo
                           select new BudgetStatsChartDto
                           {
                               Id = Guid.NewGuid(),
                               Sucursal = grupo.Key.Sucursal,
                               Fecha = new DateTime(grupo.Key.Year, grupo.Key.Month, 1).ToString("MM/yyyy"),
                               Total = grupo.Select(x => x.Estudio.Sum(x => x.PrecioFinal)).Sum(),
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

        public static List<StudiesDto> GenericStudies(this IEnumerable<RequestStudies> studies)
        {
            return studies.Select(x => new StudiesDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Estudio = x.Nombre,
                Estatus = x.Estatus,
                Precio = x.Precio,
                Descuento = x.Descuento,
                Paquete = x.Paquete,
                Promocion = x.Promocion,
                PrecioFinal = x.PrecioFinal,
            }).ToList();
        }
    }
}
