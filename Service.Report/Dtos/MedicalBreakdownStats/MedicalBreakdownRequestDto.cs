using Service.Report.Dtos.StudyStats;
using System;
using System.Collections.Generic;

namespace Service.Report.Dtos.MedicalBreakdownStats
{
    public class MedicalBreakdownRequestDto
    {
        public Guid Id { get; set; }
        public string Solicitud { get; set; }
        public string Paciente { get; set; }
        public string Medico { get; set; }
        public string ClaveMedico { get; set; }
        public Guid MedicoId { get; set; }
        public string Empresa { get; set; }
        public List<StudiesDto> Estudio { get; set; }
        public byte Estatus { get; set; }
        public decimal PrecioEstudios { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentual { get; set; }
        public decimal IVA => TotalEstudios * (decimal)0.16;
        public decimal Subtotal => TotalEstudios - IVA;
        public decimal TotalEstudios => PrecioEstudios - Descuento;
    }
}
