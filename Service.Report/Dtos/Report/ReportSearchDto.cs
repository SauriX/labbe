using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos
{
    public class ReportSearchDto
    {
        public string SucursalId { get; set; }
        public List<DateTime> Fecha { get; set; }
        public string Sucursal { get; set; }
    }
}
