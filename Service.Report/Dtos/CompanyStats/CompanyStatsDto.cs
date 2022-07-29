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
        public List<StudiesDto> Estudio { get; set; }
        public string Medico { get; set; }
        public string Empresa { get; set; }
        public byte Convenio { get; set; }
        public decimal PrecioEstudios { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentual { get; set; }
        public decimal TotalEstudios => PrecioEstudios - Descuento;
        //
        public int NoSolicitudes { get; set; }
        public decimal SumaEstudios { get; set; }
        public decimal SumaDescuentos { get; set; }
        public decimal SumaDescuentoPorcentual { get; set; }
        //
        public decimal Subtotal => Total - IVA;
        public decimal IVA => Total * (decimal)0.16;
        public decimal Total { get; set; }
    }
}
