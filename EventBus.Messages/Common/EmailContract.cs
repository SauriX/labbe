using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Common
{
    public class EmailContract
    {
        public EmailContract(string para, IEnumerable<string> paraMultiple, string asunto, string titulo, string contenido)
        {
            Para = para;
            ParaMultiple = paraMultiple;
            Asunto = asunto;
            Titulo = titulo;
            Contenido = contenido;
        }

        public string Para { get; set; }
        public IEnumerable<string> ParaMultiple { get; set; }
        public string Asunto { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
    }
}
