using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos
{
    public class InvoiceDto
    {
        public int NoSolicitudes { get; set; }
        public decimal SumaEstudios { get; set; }
        public decimal SumaDescuentos { get; set; }
        public decimal TotalDescuentoPorcentual => SumaEstudios == 0 ? 0 : (SumaDescuentos / SumaEstudios) * 100;
        public decimal Subtotal => Total - IVA;
        public decimal IVA => Total * (decimal)0.16;
        public decimal Total => SumaEstudios - SumaDescuentos;
        public decimal SubtotalCargo => Total - IVACargo;
        public decimal IVACargo => TotalCargo * (decimal)0.16;
        public decimal TotalCargo => SumaEstudios + SumaDescuentos;
    }
}
