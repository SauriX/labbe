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
                              c.Id,
                              c.NombrePaciente,
                              c.Solicitudes,
                              c.Total
                          } into grupo
                          select new PatientStatsFiltroDto
                          {
                              Id = grupo.Key.Id,
                              NombrePaciente = grupo.Key.NombrePaciente,
                              Solicitudes = grupo.Key.Solicitudes,
                              Total = grupo.Key.Total,
                          };
            return results;
        }

        public static PatientStatsFiltroDto ToPatientsStatsFormDto(this PatientStats model)
        {
            if (model == null) return null;

            return new PatientStatsFiltroDto
            {
                Id = model.Id,
                NombrePaciente = model.NombrePaciente,
                Solicitudes = model.Solicitudes,
                Total = model.Total
            };
        }

        public static List<PatientStatsFiltroDto> ToPatientStatsRecordsListDto(this List<Report.Domain.PatientStats.PatientStats> model)
        {
            if (model == null) return null;

            return model.Select(x => new PatientStatsFiltroDto
            {
                Id = x.Id,
                NombrePaciente = x.NombrePaciente,
                Solicitudes = x.Solicitudes,
                Total = x.Total,
            }).ToList();
        }
    }
}
