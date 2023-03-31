using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.MedicalRecord.Dtos.Quotation
{
    public class QuotationPdfDto
    {
        public string Sucursal { get; set; }
        public string Fecha { get; set; }
        public string FechaImpresion { get; set; }
        public string Total { get; set; }
        public string Descuento { get; set; }
        public string Iva { get; set; }
        public string TotalPago { get; set; }
        public List<QuotationPdfStudyDto> Estudios { get; set; }
    }
}