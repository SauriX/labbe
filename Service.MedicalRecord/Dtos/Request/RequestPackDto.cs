using Service.MedicalRecord.Dtos.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestPackDto
    {
        private DateTime fechaEntrega;
        public string Type => "pack";
        public int Id { get; set; }
        public Guid SolicitudId { get; set; }
        public int PaqueteId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public Guid ListaPrecioId { get; set; }
        public string ListaPrecio { get; set; }
        public int? PromocionId { get; set; }
        public string Promocion { get; set; }
        public int DepartamentoId { get; set; }
        public int AreaId { get; set; }
        public decimal Dias { get; set; }
        public int Horas { get; set; }
        public DateTime FechaEntrega { get => fechaEntrega == DateTime.MinValue ? DateTime.Now : fechaEntrega; set => fechaEntrega = value; }
        public decimal PrecioEstudios { get; set; }
        public decimal PaqueteDescuento { get; set; }
        public decimal PaqueteDescuentoProcentaje { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal PrecioFinal { get; set; }
        public bool Asignado { get; set; }
        public List<PriceListInfoPromoDto> Promociones { get; set; }
        public List<RequestStudyDto> Estudios { get; set; } = new List<RequestStudyDto>();
    }
}
