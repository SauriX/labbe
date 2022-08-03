using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.CanceledRequest
{
    public class CanceledRequestChartDto
    {
        public Guid Id { get; set; }
        public string Solicitud { get; set; }
        public int Cantidad { get; set; }
    }
}
