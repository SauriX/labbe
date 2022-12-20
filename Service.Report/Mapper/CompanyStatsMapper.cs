using Service.Report.Domain.MedicalRecord;
using Service.Report.Domain.Request;
using Service.Report.Dtos;
using Service.Report.Dtos.CompanyStats;
using Service.Report.Dtos.Request;
using Service.Report.Dtos.StudyStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Mapper
{
    public static class CompanyStatsMapper
    {
        public static IEnumerable<CompanyStatsDto> ToCompanyStatsDto(this IEnumerable<RequestInfo> model)
        {
            if (model == null) return null;

            var results = CompanyGeneric(model);

            return results;
        }

        public static IEnumerable<CompanyStatsChartDto> ToCompanyStatsChartDto(this IEnumerable<RequestInfo> model)
        {
            if (model == null) return null;

            var results = (from c in model
                           group c by new { c.CompañiaId, c.Compañia } into grupo
                           select grupo).Select(grupo =>
                           {
                               return new CompanyStatsChartDto
                               {
                                   Id = Guid.NewGuid(),
                                   Compañia = grupo.Key.Compañia,
                                   NoSolicitudes = grupo.Count(),
                               };
                           }
                          );

            return results;

        }

        public static CompanyDto ToCompanyDto(this IEnumerable<RequestInfo> model)
        {
            if (model == null) return null;

            var results = CompanyGeneric(model);

            var totals = new InvoiceDto
            {
                NoSolicitudes = results.Count(),
                SumaEstudios = results.Sum(x => x.PrecioEstudios),
                SumaDescuentos = results.Sum(x => x.Descuento),
            };

            var data = new CompanyDto
            {
                CompanyStats = results,
                CompanyTotal = totals
            };

            return data;
        }

        private static List<CompanyStatsDto> CompanyGeneric(IEnumerable<RequestInfo> model)
        {
            return model.Select(request =>
                    {
                        var studies = request.Estudios;

                        return new CompanyStatsDto
                        {
                            Id = Guid.NewGuid(),
                            Solicitud = request.Solicitud,
                            Paciente = request.NombreCompleto,
                            Medico = request.Medico,
                            Empresa = request.Compañia,
                            Convenio = request.Procedencia,
                            Estudio = studies.GenericStudies(),
                            Descuento = request.Descuento,
                            DescuentoPorcentual = request.DescuentoPorcentual,
                            Promocion = request.Promocion,
                        };
                    }).ToList();


        }
    }
}
