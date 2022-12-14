using Service.Report.Dtos.MedicalBreakdownStats;
using System.Collections.Generic;
using System;
using System.Linq;
using Service.Report.Domain.Request;
using Service.Report.Dtos.CanceledRequest;
using Service.Report.Dtos;
using Service.Report.Dtos.StudyStats;
using Service.Report.Domain.MedicalRecord;

namespace Service.Report.Mapper
{
    public static class MedicalBreakdownStatsMapper
    {
        public static IEnumerable<MedicalBreakdownRequestDto> ToMedicalBreakdownRequestDto(this IEnumerable<RequestInfo> model)
        {
            if (model == null) return null;

            var results = MedicalBreakdownGeneric(model);

            return results;
        }
        public static MedicalBreakdownDto ToMedicalBreakdownDto(this IEnumerable<RequestInfo> model)
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
        public static IEnumerable<MedicalBreakdownRequestChartDto> ToMedicalBreakdownRequestChartDto(this IEnumerable<RequestInfo> model)
        {
            if (model == null) return null;

            var results = (from c in model
                           group c by new { c.MedicoId, c.ClaveMedico } into grupo
                           select new MedicalBreakdownRequestChartDto
                           {
                               Id = Guid.NewGuid(),
                               ClaveMedico = grupo.Key.ClaveMedico,
                               NoSolicitudes = grupo.Count(),
                           });


            return results;
        }
        public static List<MedicalBreakdownRequestDto> MedicalBreakdownGeneric(IEnumerable<RequestInfo> model)
        {
            return model.OrderBy(r => r.MedicoId).Select(request =>
            {
                var studies = request.Estudios;

                return new MedicalBreakdownRequestDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Solicitud,
                    Paciente = request.NombreCompleto,
                    Medico = request.Medico,
                    ClaveMedico = request.Medico,
                    Empresa = request.Compañia,
                    Estudio = studies.GenericStudies(),
                    PrecioEstudios = request.PrecioEstudios,
                    Descuento = descount,
                    DescuentoPorcentual = request.DescuentoPorcentual,
                    MedicoId = request.MedicoId,
                    Promocion = request.Promocion,
                };
            }).ToList();
        }
    }
}
