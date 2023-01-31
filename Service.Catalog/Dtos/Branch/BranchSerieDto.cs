using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Dtos.Branch
{
    public class BranchSerieDto
    {
        public Guid SucursalId { get; set; }
        public int SerieId { get; set; }
        public string Clave { get; set; }
    }
}
