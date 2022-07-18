using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.MedicalStats
{
    public class MedicalStatsSearchDto
    {
        public string CiudadId { get; set; }
        public string SucursalId { get; set; }
        public List<DateTime> Fecha { get; set; }
        public string Sucursal { get; set; }
        public string MedicoId { get; set; }
    }
}
