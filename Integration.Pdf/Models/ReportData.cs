﻿using System.Collections.Generic;

namespace Integration.Pdf.Models
{
    public class ReportData
    {
        public List<Col> Columnas { get; set; }
        public List<ChartSeries> Series { get; set; }
        public List<Dictionary<string, object>> Datos { get; set; }
        public List<Dictionary<string, object>> DatosGrafica { get; set; }
        public List<Col> ColumnasTotales{ get; set; }
        public Dictionary<string, object> Totales { get; set; }
        public InvoiceData Invoice { get; set; }
        public HeaderData Header { get; set; }
    }
}