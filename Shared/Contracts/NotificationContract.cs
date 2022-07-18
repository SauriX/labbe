using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts
{
    public class NotificationContract
    {
        public NotificationContract(string para, IEnumerable<string> paraMultiple, string asunto, string mensaje, string @params)
        {
            Para = para;
            ParaMultiple = paraMultiple;
            Asunto = asunto;
            Mensaje = mensaje;
            Params = @params;
        }

        public string Para { get; set; }
        public IEnumerable<string> ParaMultiple { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public string Params { get; set; }
    }
}