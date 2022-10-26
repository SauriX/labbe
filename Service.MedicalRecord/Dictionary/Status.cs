namespace Service.MedicalRecord.Dictionary
{
    public class Status
    {
        public class Request
        {
            public const byte Vigente = 1;
            public const byte Completado = 2;
            public const byte Cancelado = 3;
        }

        public class RequestStudy
        {
            public const byte Pendiente = 1;
            public const byte TomaDeMuestra = 2;
            public const byte Solicitado = 3;
            public const byte Capturado = 4;
            public const byte Validado = 5;
            public const byte Liberado = 6;
            public const byte Enviado = 7;
            public const byte EnRuta = 8;
            public const byte Cancelado = 9;
            public const byte Entregado = 10;
            public const byte Urgente = 11;
        }

        public class RequestPayment
        {
            public const byte Pagado = 1;
            public const byte Facturado = 2;
            public const byte Cancelado = 3;
            public const byte FacturaCancelada = 4;
        }
    }
}
