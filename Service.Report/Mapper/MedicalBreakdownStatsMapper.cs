using Service.Report.Dtos.MedicalBreakdownStats;
using System.Collections.Generic;
using System;
using System.Linq;
using Service.Report.Domain.Request;
using Service.Report.Dtos.CanceledRequest;
using Service.Report.Dtos;
using Service.Report.Dtos.StudyStats;

namespace Service.Report.Mapper
{
    public static class MedicalBreakdownStatsMapper
    {
        public static IEnumerable<MedicalBreakdownRequestDto> ToMedicalBreakdownRequestDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = MedicalBreakdownGeneric(model);

            return results;
        }
        public static MedicalBreakdownDto ToMedicalBreakdownDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = MedicalBreakdownGeneric(model);

            var totals = new InvoiceDto
            {
                SumaEstudios = results.Sum(x => x.PrecioEstudios),
                SumaDescuentos = results.Sum(x => x.Descuento),
            };

            var data = new MedicalBreakdownDto
            {
                MedicalBreakdownRequest = results,
                MedicalBreakdownTotal = totals
            };

            return data;
        }
        public static IEnumerable<MedicalBreakdownRequestChartDto> ToMedicalBreakdownRequestChartDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            //var results = (from c in model.Where(x => x.EstatusId == 10)
            var results = (from c in model
                           group c by new { c.MedicoId, c.Medico.ClaveMedico } into grupo
                           select new MedicalBreakdownRequestChartDto
                           {
                               Id = Guid.NewGuid(),
                               ClaveMedico = grupo.Key.ClaveMedico,
                               NoSolicitudes = grupo.Count(),
                           });


            return results;
        }
        public static List<MedicalBreakdownRequestDto> MedicalBreakdownGeneric(IEnumerable<Request> model)
        {
            return model.OrderBy(r => r.MedicoId).Select(request =>
            {
                var studies = request.Estudios;
                var pack = request.Paquetes;

                var priceStudies = studies.Sum(x => x.Precio - (x.Precio * x.Paquete?.DescuentoPorcentaje ?? 0) - (x.Descuento == 0 ? 0 : x.Descuento));
                var descount = request.Descuento;
                var promotion = studies.Sum(x => x.Descuento) + pack.Sum(x => x.Descuento);
                var porcentualDescount = (descount * 100) / priceStudies;
                var descRequest = request.Descuento / 100;

                return new MedicalBreakdownRequestDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Clave,
                    Paciente = request.Expediente.Nombre,
                    Medico = request.Medico.NombreMedico,
                    ClaveMedico = request.Medico.ClaveMedico,
                    Empresa = request.Empresa.NombreEmpresa,
                    Estudio = studies.PromotionStudies(),
                    PrecioEstudios = priceStudies,
                    Descuento = descount,
                    DescuentoPorcentual = porcentualDescount,
                    MedicoId = request.Medico.Id,
                    Promocion = promotion,
                };
            }).ToList();
        }
    }
}
