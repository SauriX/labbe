namespace EventBus.Messages.Common
{
    public class NotificationContract
    {
        public NotificationContract()
        {
        }

        public NotificationContract(string mensaje, bool esAlerta,string para = "all")
        {
            Para = para;
            Mensaje = mensaje;
            EsAlerta = esAlerta;
        }

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
        public bool EsAlerta { get; set; }
    }
}