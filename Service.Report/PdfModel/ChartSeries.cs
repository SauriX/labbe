using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Report.PdfModel
{
    public class ChartSeries
    {
        public string Serie { get; set; }
        public string Formato { get; set; }
        public bool SerieX { get; set; }
        public string Color { get; set; }

        public ChartSeries() { }

        public ChartSeries(string serie)
        {
            Serie = serie;
        }

        public ChartSeries(string serie, string color)
        {
            Serie = serie;
            Color = color;
        }

        public ChartSeries(string serie, bool serieX)
        {
            Serie = serie;
            SerieX = serieX;
        }

        public ChartSeries(string serie, string color, string format)
        {
            Serie = serie;
            Color = color;
            Formato = format;
        }
    }

}