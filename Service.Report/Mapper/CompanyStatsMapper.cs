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
        public static IEnumerable<CompanyStatsDto> ToCompanyStatsDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = CompanyGeneric(model);

            return results;
        }

        public static IEnumerable<CompanyStatsChartDto> ToCompanyStatsChartDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = (from c in model
                           group c by new { c.EmpresaId, c.Empresa.NombreEmpresa } into grupo
                           select grupo).Select(grupo =>
                           {
                               return new CompanyStatsChartDto
                               {
                                   Id = Guid.NewGuid(),
                                   Compañia = grupo.Key.NombreEmpresa,
                                   NoSolicitudes = grupo.Count(),
                               };
                           }
                          );

            return results;

        }

        public static CompanyDto ToCompanyDto(this IEnumerable<Request> model)
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

        public static List<StudiesDto> PromotionStudies(this IEnumerable<RequestStudy> studies)
        {
            return studies.Select(x => new StudiesDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Estudio = x.Estudio,
                Estatus = x.Estatus.Estatus,
                Precio = x.Precio,
                Descuento = x.Descuento,
                Paquete = x.Paquete?.Nombre,
                Promocion = x.Paquete?.Descuento / studies.Count(),
                PrecioFinal = x.Precio - (x.Precio * x.Paquete?.DescuentoPorcentaje ?? 0) - (x.Descuento == 0 ? 0 : x.Descuento),
            }).ToList();
        }

        private static List<CompanyStatsDto> CompanyGeneric(IEnumerable<Request> model)
        {
            return model.Select(request =>
                    {
                        var studies = request.Estudios;
                        var pack = request.Paquetes;

                        var priceStudies = studies.Sum(x => x.Precio - (x.Precio * x.Paquete?.DescuentoPorcentaje ?? 0) - (x.Descuento == 0 ? 0 : x.Descuento));
                        var descount = request.Descuento;
                        var promotion = studies.Sum(x => x.Descuento) + pack.Sum(x => x.Descuento);
                        var porcentualDescount = (descount * 100) / priceStudies;
                        var descRequest = request.Descuento / 100;

                        return new CompanyStatsDto
                        {
                            Id = Guid.NewGuid(),
                            Solicitud = request.Clave,
                            Paciente = request.Expediente.Nombre,
                            Medico = request.Medico.NombreMedico,
                            Empresa = request.Empresa.NombreEmpresa,
                            Convenio = request.Empresa.Convenio,
                            Estudio = studies.PromotionStudies(),
                            Descuento = descount,
                            DescuentoPorcentual = porcentualDescount,
                            Promocion = promotion,
                        };
                    }).ToList();


        }
    }
}
