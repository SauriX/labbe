using System;

namespace Service.MedicalRecord.Dtos.PriceQuote
{
    public class PriceQuoteExpedienteSearch
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Buscar { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }
}
