using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Dtos.PriceQuote
{
    public class PriceQuoteDto
    {
        public string Sucursal { get; set; }
        public string Fecha { get; set; }
        public string FechaImpresion { get; set; }

        public List<StudyQuoteDto> StudyQuotes { get; set; }

        public string Total { get; set; }
        public string Descuento { get; set; }
        public string Iva { get; set; }
        public string Totalpagar {get; set;}
    }
}