using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos
{
    public class ReportFiltroDto
    {
        public List<string> SucursalId { get; set; }
        public List<DateTime> Fecha { get; set; }
        public string Sucursal { get; set; }
        public string MedicoId { get; set; }
    }
}
