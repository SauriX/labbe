using System;

namespace Service.Sender.Domain.NotificationHistory
{
    public class NotificationHistory
    {
        public Guid Id { get; set; }
        public string Para { get; set; }
        public string Mensaje { get; set; }
        public bool EsAlerta { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
