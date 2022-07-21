using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Common
{
    public class NotificationContract
    {
        public NotificationContract(string para, string asunto, string mensaje, string @params)
        {
            Para = para;
            Asunto = asunto;
            Mensaje = mensaje;
            Params = @params;
        }

        public string Para { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public string Params { get; set; }
    }
}