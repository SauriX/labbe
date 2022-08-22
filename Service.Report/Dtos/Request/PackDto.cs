using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.Request
{
    public class PackDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Promocion { get; set; }
        public decimal DescuentoPorcentual { get; set; }
    }
}
