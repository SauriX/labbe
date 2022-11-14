using System;

namespace Service.MedicalRecord.Dtos.PriceQuote
{
    public class PriceQuoteGeneralDto
    {
        public Guid CotizacionId { get; set; }
        public string Procedencia { get; set; }
        public string Compañia { get; set; }
        public string Medico { get; set; }
        public string NomprePaciente { get; set; }

        public string Observaciones { get; set; }
        public string TipoEnvio { get; set; }
        public string Email { get; set; }
        public string Whatssap { get; set; }
        public bool Activo { get; set; }
    }
}
