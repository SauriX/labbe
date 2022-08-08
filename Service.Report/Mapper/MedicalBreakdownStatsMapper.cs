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
                var priceStudies = request.Precio;
                var descount = request.Descuento;
                var porcentualDescount = Math.Round(descount * 100) / priceStudies;
                var descRequest = request.Descuento / 100;

                return new MedicalBreakdownRequestDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Clave,
                    Paciente = request.Expediente.Nombre,
                    Medico = request.Medico.NombreMedico,
                    ClaveMedico = request.Medico.ClaveMedico,
                    Empresa = request.Empresa.NombreEmpresa,
                    Estudio = studies.ToRequestMedicalBreakdown(descRequest),
                    PrecioEstudios = priceStudies,
                    Descuento = descount,
                    DescuentoPorcentual = porcentualDescount,
                    MedicoId = request.Medico.Id
                };
            }).ToList();
        }
        public static List<StudiesDto> ToRequestMedicalBreakdown(this IEnumerable<RequestStudy> studies, decimal descuento)
        {
            return studies.Select(x => new StudiesDto
            { 
                Id = Guid.NewGuid(),
                Clave = x.Clave,
                Estudio = x.Estudio,
                Estatus = x.Estatus.Estatus,
                Precio = x.Precio,
                PrecioFinal = x.PrecioFinal - (x.Precio * descuento),
            }).ToList();
        }
    }
}
