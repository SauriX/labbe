using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.PriceQuote
{
    public class PriceQuoteFilterDto
    {
        public DateTime? FechaAInicial { get; set; }
        public DateTime? FechaAFinal { get; set; }
        public List<string> Sucursales { get; set; }
        public string CorreoTelefono { get; set; }
        public DateTime? FechaNInicial { get; set; }
        public DateTime? FechaNFinal { get; set; }
        public string Expediente { get; set; }
    }
}
