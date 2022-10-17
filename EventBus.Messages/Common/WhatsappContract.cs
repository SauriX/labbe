using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Common
{
    public class WhatsappContract
    {
        public WhatsappContract(string telefono, string mensaje, List<SenderFiles> senderFiles = null)
        {
            Telefono = telefono;
            Mensaje = mensaje;
            SenderFiles = senderFiles;
            
        }

        public string Telefono { get; set; }
        public string Mensaje { get; set; }
        public List<SenderFiles> SenderFiles { get; set; }
        public string RemitenteId { get; set; }
        public bool Notificar { get; set; }
    }

}
