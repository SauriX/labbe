using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.TypeRequest
{
    public class TypeRequestChartDto
    {
        public Guid Id { get; set; }
        public string Sucursal { get; set; }
        public int Cantidad { get; set; }
    }
}
