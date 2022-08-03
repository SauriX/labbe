using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Common
{
    public class WhatsappContract
    {
        public WhatsappContract(string telefono, string mensaje)
        {
            Telefono = telefono;
            Mensaje = mensaje;
        }

        public string Telefono { get; set; }
        public string Mensaje { get; set; }
        public string RemitenteId { get; set; }
        public bool Notificar { get; set; }
    }
}
