namespace Service.MedicalRecord.Dtos.Promotion
{
    public class PriceListInfoPromoDto
    {
        public PriceListInfoPromoDto(int estudioId, int paqueteId, int? promocionId, string promocion, decimal descuento, decimal descuentoPorcentaje)
        {
            EstudioId = estudioId;
            PaqueteId = paqueteId;
            PromocionId = promocionId;
            Promocion = promocion;
            Descuento = descuento;
            DescuentoPorcentaje = descuentoPorcentaje;
        }

        public int EstudioId { get; set; }
        public int PaqueteId { get; set; }
        public int? PromocionId { get; set; }
        public string Promocion { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
    }
}
