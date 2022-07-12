using System;
using System.Collections.Generic;

namespace Service.Report.Dtos.PatientStats
{
    public class PatientStatsSearchDto
    {
        public string CiudadId { get; set; }
        public string SucursalId { get; set; }
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public List<DateTime> Fecha { get; set; }
    }
}
