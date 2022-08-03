using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.CompanyStats
{
    public class CompanyStatsTotalDto
    {
        public int NoSolicitudes { get; set; }
        public decimal SumaEstudios { get; set; }
        public decimal SumaDescuentos { get; set; }
        public double SumaDescuentoPorcentual => ((double)SumaDescuentos / (double)SumaEstudios);
        public decimal TotalDescuentoPorcentual => (decimal)SumaDescuentoPorcentual;
        public decimal Subtotal => Total - IVA;
        public decimal IVA => Total * (decimal)0.16;
        public decimal Total => SumaEstudios - SumaDescuentos;
    }
}
