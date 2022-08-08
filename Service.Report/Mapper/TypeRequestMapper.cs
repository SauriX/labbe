using Service.Report.Domain.Request;
using Service.Report.Dtos;
using Service.Report.Dtos.StudyStats;
using Service.Report.Dtos.TypeRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Mapper
{
    public static class TypeRequestMapper
    {
        public static IEnumerable<TypeRequestDto> ToDescountRequestDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = DescountGeneric(model);

            return results;
        }

        public static TypeDto ToDescountDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = DescountGeneric(model);

            var totals = new InvoiceDto
            {
                SumaEstudios = results.Sum(x => x.PrecioEstudios),
                SumaDescuentos = results.Sum(x => x.Descuento),
            };

            var data = new TypeDto
            {
                TypeDescountRequest = results,
                TypeDescountTotal = totals
            };

            return data;
        }

        public static IEnumerable<TypeRequestChartDto> ToDescountRequestChartDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = (from c in model.Where(x => x.Descuento != 0)
                           group c by new { c.SucursalId, c.Sucursal.Sucursal } into grupo
                           select new TypeRequestChartDto
                           {
                               Id = Guid.NewGuid(),
                               Sucursal = grupo.Key.Sucursal,
                               Cantidad = grupo.Count(),
                           });


            return results;
        }

        public static List<TypeRequestDto> DescountGeneric(IEnumerable<Request> model)
        {
            return model.Where(x => x.Descuento != 0).Select(request =>
            {
                var studies = request.Estudios;
                var priceStudies = request.Precio;
                var descount = request.Descuento;
                var porcentualDescount = (descount * 100) / priceStudies;
                var descRequest = request.Descuento / 100;

                return new TypeRequestDto
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

        public static IEnumerable<TypeRequestDto> ToChargeRequestDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = ChargeGeneric(model);

            return results;
        }

        public static TypeDto ToChargeDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = ChargeGeneric(model);

            var totals = new InvoiceDto
            {
                SumaEstudios = results.Sum(x => x.PrecioEstudios),
                SumaDescuentos = results.Sum(x => x.Cargo),
            };

            var data = new TypeDto
            {
                TypeChargeRequest = results,
                TypeChargeTotal = totals
            };

            return data;
        }

        public static IEnumerable<TypeRequestChartDto> ToChargeRequestChartDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = (from c in model.Where(x => x.Cargo != 0)
                           group c by new { c.SucursalId, c.Sucursal.Sucursal } into grupo
                           select new TypeRequestChartDto
                           {
                               Id = Guid.NewGuid(),
                               Sucursal = grupo.Key.Sucursal,
                               Cantidad = grupo.Count(),
                           });


            return results;
        }

        public static List<TypeRequestDto> ChargeGeneric(IEnumerable<Request> model)
        {
            return model.Where(x => x.Cargo != 0).Select(request =>
            {
                var studies = request.Estudios;
                var priceStudies = request.Precio;
                var charge = request.Cargo;
                var porcentualCharge = (charge * 100) / priceStudies;
                var chargeRequest = request.Cargo / 100;

                return new TypeRequestDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Clave,
                    Paciente = request.Expediente.Nombre,
                    Medico = request.Medico.NombreMedico,
                    Empresa = request.Empresa.NombreEmpresa,
                    Estudio = studies.ToRequest(chargeRequest),
                    PrecioEstudios = priceStudies,
                    Cargo = charge,
                    CargoPorcentual = porcentualCharge,
                };
            }).ToList();
        }
    }
}

