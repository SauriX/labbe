using System.Collections.Generic;
using System;
using System.Linq;

namespace Service.Report.Domain.MedicalRecord
{
    public class Quotation
    {
        public Guid Id { get; set; }
        public string ClaveSolicitud { get; set; }
        public string NombrePaciente { get; set; }
        public string Solicitud { get; set; }
        public string Sucursal { get; set; }
        public string NombreMedico { get; set; }
        public decimal PrecioEstudios => Estudio.Sum(x => x.PrecioFinal);
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentual { get; set; }
        public decimal Promocion { get; set; }
        public decimal IVA => TotalEstudios * (decimal)0.16;
        public decimal Subtotal => TotalEstudios - IVA;
        public decimal TotalEstudios => PrecioEstudios - Descuento - Promocion;
        public List<RequestStudies> Estudio { get; set; }
    }
}
