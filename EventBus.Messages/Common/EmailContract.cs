﻿using System.Collections.Generic;

namespace EventBus.Messages.Common
{
    public class EmailContract
    {
        public EmailContract()
        {
        }

        public EmailContract(string para, string asunto, string titulo, string contenido, List<SenderFiles> senderFiles = null)
        {
            Para = para;
            Asunto = asunto;
            Titulo = titulo;
            Contenido = contenido;
            SenderFiles = senderFiles;

        }

        public EmailContract(IEnumerable<string> paraMultiple, string asunto, string titulo, string contenido, List<SenderFiles> senderFiles = null)
        {
            ParaMultiple = paraMultiple;
            Asunto = asunto;
            Titulo = titulo;
            Contenido = contenido;
            SenderFiles = senderFiles;

        }

        public string Para { get; set; }
        public IEnumerable<string> ParaMultiple { get; set; }
        public string Asunto { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public List<SenderFiles> SenderFiles { get; set; }
        public string RemitenteId { get; set; }
        public bool Notificar { get; set; }
        public bool CorreoIndividual { get; set; }
    }
}
