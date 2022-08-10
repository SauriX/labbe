using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dictionary
{
    public class Payment
    {
        public class RequestPayment
        {
            public const byte Pagado = 1;
            public const byte Facturado = 2;
            public const byte Cancelado = 3;
        }
    }
}
