using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.PdfModel
{
    public class TotalData
    {
        public int NoSolicitudes { get; set; }
        public decimal Precios { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentual { get; set; }
        public decimal Total { get; set; }
    }
}
