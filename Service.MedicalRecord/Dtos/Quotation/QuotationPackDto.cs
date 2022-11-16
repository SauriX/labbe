using Service.MedicalRecord.Dtos.Request;
using System.Collections.Generic;
using System;
using Service.MedicalRecord.Dtos.Promotion;

namespace Service.MedicalRecord.Dtos.Quotation
{
    public class QuotationPackDto
    {
        public string Type => "pack";
        public int Id { get; set; }
        public Guid CotizacionId { get; set; }
        public int PaqueteId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public Guid ListaPrecioId { get; set; }
        public string ListaPrecio { get; set; }
        public int? PromocionId { get; set; }
        public string Promocion { get; set; }
        public bool AplicaCargo { get; set; }
        public decimal Dias { get; set; }
        public int Horas { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal PrecioFinal { get; set; }
        public List<PriceListInfoPromoDto> Promociones { get; set; }
        public List<QuotationStudyDto> Estudios { get; set; } = new List<QuotationStudyDto>();
    }
}
