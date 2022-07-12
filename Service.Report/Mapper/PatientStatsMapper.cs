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
                              c.Nombre,
                              c.Solicitado,
                              c.Monto
                          } into grupo
                          select new PatientStatsFiltroDto
                          {
                              Id = grupo.Key.Id,
                              Nombre = grupo.Key.Nombre,
                              Solicitado = grupo.Key.Solicitado,
                              Monto = grupo.Key.Monto,
                          };
            return results;
        }

        public static PatientStatsFiltroDto ToPatientsStatsFormDto(this PatientStats model)
        {
            if (model == null) return null;

            return new PatientStatsFiltroDto
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Solicitado = model.Solicitado,
                Monto = model.Monto
            };
        }

        public static List<PatientStatsFiltroDto> ToPatientStatsRecordsListDto(this List<Report.Domain.PatientStats.PatientStats> model)
        {
            if (model == null) return null;

            return model.Select(x => new PatientStatsFiltroDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Solicitado = x.Solicitado,
                Monto = x.Monto,
            }).ToList();
        }
    }
}
