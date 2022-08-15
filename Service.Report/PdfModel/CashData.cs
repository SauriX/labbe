using Service.Report.PdfModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Models
{
    public class CashData
    {
        public List<Col> Columnas { get; set; }
        public List<Dictionary<string, object>> PerDay { get; set; }
        public List<Dictionary<string, object>> Canceled { get; set; }
        public List<Dictionary<string, object>> OtherDay { get; set; }
        public List<Col> ColumnasTotales { get; set; }
        public Dictionary<string, object> Totales { get; set; }
        public InvoiceData Invoice { get; set; }
        public HeaderData Header { get; set; }
    }
}