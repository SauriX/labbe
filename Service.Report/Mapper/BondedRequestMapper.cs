using Service.Report.Domain.Request;
using Service.Report.Dtos;
using Service.Report.Dtos.StudyStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Mapper
{
    public static class BondedRequestMapper
    {
        public static List<BondedRequestDto> ToBondedInternDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;
            var results = BondedGeneric(model);

            return results;
        }

        public static List<BondedRequestDto> ToBondedExternalDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;
            var results = BondedGeneric(model);

            return results;
        }

        private static List<BondedRequestDto> BondedGeneric(IEnumerable<Request> model)
        {
            return model.Select(request =>
            {
                var studies = request.Estudios;

                return new BondedRequestDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Clave,
                    Paciente = request.Expediente.Nombre,
                    Edad = request.Expediente.Edad,
                    Sexo = request.Expediente.Sexo,
                    Estudio = studies.ToStudiesDto(),
                    Medico = request.Medico.NombreMedico,
                    FechaEntrega = request.Fecha.ToString("dd/MM/yyyy"),
                };
            }).ToList();
        }
    }
}
