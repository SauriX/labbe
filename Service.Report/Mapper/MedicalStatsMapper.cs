﻿using Service.Report.Domain.MedicalRecord;
using Service.Report.Domain.Request;
using Service.Report.Dtos.MedicalStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Mapper
{
    public static class MedicalStatsMapper
    {
        public static IEnumerable<MedicalStatsDto> ToMedicalStatsDto(this IEnumerable<RequestInfo> model)
        {
            if (model == null) return null;

            var results = (from c in model
                           group c by new { c.Medico, c.ClaveMedico, c.MedicoId } into grupo
                           select new MedicalStatsDto
                           {
                               Id = Guid.NewGuid(),
                               ClaveMedico = grupo.Key.ClaveMedico,
                               Medico = grupo.Key.Medico,
                               Total = grupo.Sum(x => x.TotalEstudios),
                               NoSolicitudes = grupo.Count(),
                               NoPacientes = grupo.Select(x => x.ExpedienteId).Distinct().Count(),
                           }).ToList();

            results.Add(new MedicalStatsDto
            {
                Id = Guid.NewGuid(),
                ClaveMedico = "Total",
                Medico = " ",
                Total = results.Sum(x => x.Total),
                NoSolicitudes = results.Sum(x => x.NoSolicitudes),
                NoPacientes = results.Sum(x => x.NoPacientes),
            });

            return results;

        }
    }
}
