using Service.Report.Dtos.StudyStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.CompanyStats
{
    public class CompanyStatsDto
    {
        public Guid Id { get; set; }
        public string Solicitud { get; set; }
        public string Paciente { get; set; }
        public List<StudiesDto> Estudio { get; set; } = new List<StudiesDto>();
        public string Medico { get; set; }
        public string Empresa { get; set; }
        public byte Convenio { get; set; }
        public decimal PrecioEstudios => Estudio.Sum(x => x.PrecioFinal);
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentual { get; set; }
        public decimal? Promocion { get; set; }
        public decimal TotalEstudios => PrecioEstudios - Descuento - Promocion ?? 0;
    }
}
