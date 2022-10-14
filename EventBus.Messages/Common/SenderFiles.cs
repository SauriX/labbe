using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Common
{
    public class SenderFiles
    {
        public Uri Ruta { get; set; }
        public string Nombre { get; set; }
        public SenderFiles(Uri ruta, string nombre)
        {
            Ruta = ruta;
            Nombre = nombre;
        }
    }
}
