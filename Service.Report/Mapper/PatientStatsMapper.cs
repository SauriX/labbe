using Service.Report.Domain.PatientStats;
using Service.Report.Dtos.PatientStats;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Report.Mapper
{
    public static class PatientStatsMapper
    {
        public static IEnumerable<PatientStatsFiltroDto> ToPatientStatsListDto(this List<PatientStats> model)
        {
            if (model == null) return null;

            var results = from c in model
                          group c by new
                          {
                              c.NombrePaciente,
                          } into grupo
                          select new PatientStatsFiltroDto
                          {
                              NombrePaciente = grupo.Key.NombrePaciente,
                              Solicitudes = grupo.Count(),
                              Total = grupo.Sum(x => x.Total)
                          };
            return results;
        }

        public static PatientStatsFiltroDto ToPatientsStatsFormDto(this PatientStats model)
        {
            if (model == null) return null;

            return new PatientStatsFiltroDto
            {
                NombrePaciente = model.NombrePaciente,
            };
        }

        public static List<PatientStatsFiltroDto> ToPatientStatsRecordsListDto(this List<Report.Domain.PatientStats.PatientStats> model)
        {
            if (model == null) return null;

            return model.Select(x => new PatientStatsFiltroDto
            {
                NombrePaciente = x.NombrePaciente,
            }).ToList();
        }
    }
}
