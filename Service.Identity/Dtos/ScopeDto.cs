using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Identity.Dtos
{
    public class ScopeDto
    {
        public string Pantalla { get; set; }
        public bool Acceder { get; set; }
        public bool Crear { get; set; }
        public bool Editar { get; set; }
        public bool Descargar { get; set; }
    }
}
