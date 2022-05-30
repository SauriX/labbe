using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Identity.Dtos.Scopes
{
    public class ScopesDto
    {
        public string Pantalla { get; set; }
        public bool Acceder { get; set; }
        public bool Crear { get; set; }
        public bool Modificar { get; set; }
        public bool Descargar { get; set; }
        public bool Imprimir { get; set; }
        public bool EnviarCorreo { get; set; }
        public bool EnviarWapp { get; set; }
    }
}
