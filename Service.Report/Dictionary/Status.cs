using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dictionary
{
    public class Status
    {
        public class Request
        {
            public const byte Pendiente = 1;
            public const byte TomaDeMuestra = 2;
            public const byte Solicitado = 3;
            public const byte Capturado = 4;
            public const byte Validado = 5;
            public const byte EnRuta = 6;
            public const byte Liberado = 7;
            public const byte Enviado = 8;
            public const byte Entregado = 9;
            public const byte Cancelado = 10;
            public const byte Urgente = 11;
        }
    }
}
