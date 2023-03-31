﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Dtos.PriceQuote
{
    public class QuotationTicketStudyDto
    {
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Precio { get; set; }
        public string Descuento { get; set; }
        public string IVA { get; set; }
        public string PrecioFinal { get; set; }
        public string TiempoEntrega { get; set; }
        public string TipoMuestra { get; set; }
        public string PreparacionPaciente { get; set; }
    }
}