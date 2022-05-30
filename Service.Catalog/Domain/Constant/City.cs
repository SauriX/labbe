using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Domain.Constant
{
    public class City
    {
        public short Id { get; set; }
        public byte EstadoId { get; set; }
        public virtual State Estado { get; set; }
        public string Ciudad { get; set; }
    }
}
