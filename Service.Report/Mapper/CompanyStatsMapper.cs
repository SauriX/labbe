using Service.Report.Domain.Request;
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

            var results = (from c in model
                           group c by new { c.Clave, c.Expediente.Nombre, c.Medico.NombreMedico, c.Empresa.Id, c.Empresa.NombreEmpresa, c.Empresa.Convenio } into grupo
                           select grupo).Select(grupo =>
                           {
                               var studies = grupo.SelectMany(x => x.Estudios);

                               var priceStudies = studies.Sum(x => x.PrecioFinal);
                               var descount = studies.Sum(x => x.Descuento);
                               var porcentualDescount = (descount * 100) / priceStudies;
                               decimal sumStudies = 0;
                               var sum = sumStudies += priceStudies;

                               return new CompanyStatsDto
                               {
                                   Id = Guid.NewGuid(),
                                   Solicitud = grupo.Key.Clave,
                                   Paciente = grupo.Key.Nombre,
                                   Medico = grupo.Key.NombreMedico,
                                   Empresa = grupo.Key.NombreEmpresa,
                                   Convenio = grupo.Key.Convenio,
                                   Estudio = studies.ToCompanyDto(),
                                   PrecioEstudios = priceStudies,
                                   Descuento = descount,
                                   DescuentoPorcentual = porcentualDescount,
                                   NoSolicitudes = grupo.Count(),
                               };
                           }
                           ).ToList();

            results.Add(new CompanyStatsDto
            {
                SumaEstudios = results.Sum(x => x.PrecioEstudios),
                SumaDescuentos = results.Sum(x => x.Descuento),
                SumaDescuentoPorcentual = results.Sum(x => x.DescuentoPorcentual),
            });



            return results;
        }

        public static List<StudiesDto> ToCompanyDto(this IEnumerable<RequestStudy> studies)
        {
            return studies.Select(x => new StudiesDto
            {
                Clave = x.Clave,
                Estudio = x.Estudio,
                Estatus = x.Estatus.Estatus,
                Precio = x.PrecioFinal,
            }).ToList();
        }
    }
}
