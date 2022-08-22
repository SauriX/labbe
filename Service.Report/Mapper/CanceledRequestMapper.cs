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
        public static IEnumerable<CanceledRequestDto> ToCanceledRequestDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = CanceledGeneric(model);

            return results;
        }

        public static CanceledDto ToCanceledDto(this IEnumerable<Request> model)
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

        public static IEnumerable<CanceledRequestChartDto> ToCanceledRequestChartDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = (from c in model.Where(x => x.EstatusId == 10)
                           group c by new { c.SucursalId, c.Sucursal.Sucursal } into grupo
                           select new CanceledRequestChartDto
                           {
                               Id = Guid.NewGuid(),
                               Sucursal = grupo.Key.Sucursal,
                               Cantidad = grupo.Count(),
                           });


            return results;
        }

        public static List<CanceledRequestDto> CanceledGeneric(IEnumerable<Request> model)
        {
            return model.Where(x => x.EstatusId == 10).Select(request =>
            {
                var studies = request.Estudios;
                var pack = request.Paquetes;

                var priceStudies = studies.Sum(x => x.Precio - (x.Precio * x.Paquete?.DescuentoPorcentaje ?? 0) - (x.Descuento == 0 ? 0 : x.Descuento));
                var descount = request.Descuento;
                var promotion = studies.Sum(x => x.Descuento) + pack.Sum(x => x.Descuento);
                var porcentualDescount = (descount * 100) / priceStudies;
                var descRequest = request.Descuento / 100;

                return new CanceledRequestDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Clave,
                    Paciente = request.Expediente.Nombre,
                    Medico = request.Medico.NombreMedico,
                    Empresa = request.Empresa.NombreEmpresa,
                    Estudio = studies.PromotionStudies(),
                    Descuento = descount,
                    DescuentoPorcentual = porcentualDescount,
                };
            }).ToList();
        }
    }
}
