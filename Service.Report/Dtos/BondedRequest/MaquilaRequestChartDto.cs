using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.BondedRequest
{
    public class MaquilaRequestChartDto
    {
        public Guid Id { get; set; }
        public string Maquila { get; set; }
        public int NoSolicitudes { get; set; }
    }
}
