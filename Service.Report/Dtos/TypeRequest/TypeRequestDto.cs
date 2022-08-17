using Service.Report.Dtos.StudyStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.TypeRequest
{
    public class TypeRequestDto
    {
        public Guid Id { get; set; }
        public string Solicitud { get; set; }
        public string Paciente { get; set; }
        public string Medico { get; set; }
        public string Empresa { get; set; }
        public List<StudiesDto> Estudio { get; set; }
        public decimal PrecioEstudios => Estudio.Sum(x => x.PrecioFinal);
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentual { get; set; }
        public decimal IVA => TotalEstudios * (decimal)0.16;
        public decimal Subtotal => TotalEstudios - IVA;
        public decimal TotalEstudios => PrecioEstudios - Descuento - Promocion;
        public decimal Cargo { get; set; }
        public decimal CargoPorcentual { get; set; }
        public decimal Promocion { get; set; }
        public decimal IVACargo => TotalCargo * (decimal)0.16;
        public decimal SubtotalCargo => TotalCargo - IVACargo;
        public decimal TotalCargo => PrecioEstudios + Cargo - Promocion;
    }
}
