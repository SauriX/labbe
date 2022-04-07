using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Domain.Catalog
{
    public class Dimension
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public byte Largo { get; set; }
        public byte Ancho { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModificoId { get; set; }
        public DateTime FechaModifico { get; set; }
    }
}
