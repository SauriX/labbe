using Service.Report.Domain.Request;
using Service.Report.Dtos;
using Service.Report.Dtos.CompanyStats;
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

        public static List<StudiesDto> ToCompanyDto(this IEnumerable<RequestStudy> studies, decimal descuento)
        {
            return studies.Select(x => new StudiesDto
            {
                Clave = x.Clave,
                Estudio = x.Estudio,
                Estatus = x.Estatus.Estatus,
                Precio = x.Precio,
                PrecioFinal = x.PrecioFinal - (x.Precio * descuento),
            }).ToList();
        }

        private static List<CompanyStatsDto> CompanyGeneric(IEnumerable<Request> model)
        {
            return model.Select(request =>
                    {
                        var studies = request.Estudios;

                        var priceStudies = request.Precio;
                        var descount = request.Descuento;
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
                            Estudio = studies.ToCompanyDto(descRequest),
                            PrecioEstudios = priceStudies,
                            Descuento = descount,
                            DescuentoPorcentual = porcentualDescount,
                        };
                    }).ToList();
        }
    }
}
