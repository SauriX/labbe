using System.Collections.Generic;

namespace Service.Billing.Dtos.Notification
{
    public class NotificationDto
    {
        public string Usuario { get; set; }
        public string Metodo { get; set; }
        public string Mensaje { get; set; }
        public Dictionary<string, object> Datos { get; set; }
    }
}
