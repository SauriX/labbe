using System;

namespace Service.Report.Domain.MedicalRecord
{
    public class RequestStudies
    {
        private DateTime fechaEntrega;

        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int? DepartamentoId { get; set; }
        public int? AreaId { get; set; }
        public byte EstatusId { get; set; }
        public string Estatus { get; set; }
        public decimal? Promocion { get; set; }
        public int? PaqueteId { get; set; }
        public string Paquete { get; set; }
        public decimal Dias { get; set; }
        public int Horas { get; set; }
        public DateTime FechaEntrega { get => fechaEntrega == DateTime.MinValue ? DateTime.Now : fechaEntrega; set => fechaEntrega = value; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal PrecioFinal { get; set; }
    }
}
