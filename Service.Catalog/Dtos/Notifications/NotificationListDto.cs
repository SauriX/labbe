using System;

namespace Service.Catalog.Dtos.Notifications
{
    public class NotificationListDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Titulo { get; set; }
        public string Fecha { get; set; }
        public bool Activo { get; set; }
        public string Contenido { get; set; }
        public string Tipo { get; set;}
    }
}
