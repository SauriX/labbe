using System.Collections.Generic;
using System;
using System.Linq;
using Service.MedicalRecord.Dtos.Reports.StudyStats;

namespace Service.MedicalRecord.Dtos.Reports.BudgetStats
{
    public class BudgetStatsDto
    {
        public Guid Id { get; set; }
        public string ClaveSolicitud { get; set; }
        public string NombrePaciente { get; set; }
        public string Solicitud { get; set; }
        public Guid SucursalId { get; set; }
        public string Sucursal { get; set; }
        public string NombreMedico { get; set; }
        public decimal PrecioEstudios => Estudio.Sum(x => x.PrecioFinal);
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentual { get; set; }
        public decimal Promocion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal IVA => TotalEstudios * (decimal)0.16;
        public decimal Subtotal => TotalEstudios - IVA;
        public decimal TotalEstudios => PrecioEstudios - Descuento - Promocion;
        public List<StudiesDto> Estudio { get; set; }
    }
}
