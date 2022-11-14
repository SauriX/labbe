using Service.MedicalRecord.Dtos.Request;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.PriceQuote
{
    public class PriceQuoteInfoDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Expediente { get; set; }
        public string Paciente { get; set; }
        public string Correo { get; set; }
        public string Whatsapp { get; set; }
        public DateTime Fecha { get; set; }
        public bool Activo { get; set; }
        public IEnumerable<PriceQuoteStudyInfoDto> Estudios { get; set; }
    }

    public class PriceQuoteStudyInfoDto
    {
        public Guid Id { get; set; }
        public int EstudioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
    }
}