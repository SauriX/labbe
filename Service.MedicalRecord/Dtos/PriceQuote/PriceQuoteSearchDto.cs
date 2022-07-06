using System;

namespace Service.MedicalRecord.Dtos.PriceQuote
{
    public class PriceQuoteSearchDto
    {
        public string Presupuesto   { get; set; }
        public bool Activo { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Ciudad { get; set; }
        public string Sucursal { get; set; }
        public string Email { get; set; }
    }
}
