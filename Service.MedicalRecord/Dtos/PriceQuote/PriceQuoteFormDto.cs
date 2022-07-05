using System;

namespace Service.MedicalRecord.Dtos.PriceQuote
{
    public class PriceQuoteFormDto
    {
        public string Id { get; set; }
        public string expediente { get; set; }
        public string expedienteid { get; set; }
        public string nomprePaciente { get; set; }
        public Decimal edad { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public PriceQuoteGeneralDto generales { get; set; }
        public string Genero { get; set; }
        public Guid UserId { get; set; }
    }
}
