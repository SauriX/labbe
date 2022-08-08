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
                var priceStudies = request.Precio;
                var descount = request.Descuento;
                var porcentualDescount = (descount * 100) / priceStudies;
                var descRequest = request.Descuento / 100;

                return new CanceledRequestDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Clave,
                    Paciente = request.Expediente.Nombre,
                    Medico = request.Medico.NombreMedico,
                    Empresa = request.Empresa.NombreEmpresa,
                    Estudio = studies.ToRequest(descRequest),
                    PrecioEstudios = priceStudies,
                    Descuento = descount,
                    DescuentoPorcentual = porcentualDescount,
                };
            }).ToList();
        }

        public static List<StudiesDto> ToRequest(this IEnumerable<RequestStudy> studies, decimal descuento)
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
    }
}
