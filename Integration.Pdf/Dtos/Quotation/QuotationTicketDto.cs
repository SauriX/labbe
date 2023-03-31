using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Dtos.PriceQuote
{
    public class QuotationTicketDto
    {
        public string Sucursal { get; set; }
        public string Fecha { get; set; }
        public string FechaImpresion { get; set; }
        public string Total { get; set; }
        public string Descuento { get; set; }
        public string IVA { get; set; }
        public string TotalPago { get; set; }
        public List<QuotationTicketStudyDto> Estudios { get; set; }
    }
}