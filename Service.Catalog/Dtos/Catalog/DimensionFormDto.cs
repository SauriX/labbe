using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Dtos.Catalog
{
    public class DimensionFormDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public byte Largo { get; set; }
        public byte Ancho { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
