using System;

namespace Service.MedicalRecord.Dtos.PriceQuote
{
    public class PriceQuoteListDto
    {
        public string Id { get; set; }
        public string Presupuesto { get; set; }
        public string  NomprePaciente { get; set; }
        public string Estudios { get; set; }
        public string Email { get; set; }
        public string Whatsapp { get; set; }
        public DateTime Fecha { get; set; }
        public string Expediente { get; set; }
        public bool Activo { get; set; }
    }
}
