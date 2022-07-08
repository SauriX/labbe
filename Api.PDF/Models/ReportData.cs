using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.PDF.Models
{
    public class ReportData
    {
        public List<Col> Columnas { get; set; }
        public List<ChartSeries> Series { get; set; }
        public List<Dictionary<string, object>> Datos { get; set; }
    }
}