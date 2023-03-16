using System;

namespace Service.Catalog.Domain.Notifications
{
    public class Notification_Role
    {
        public Guid NotificacionId { get; set; }
        public virtual Notifications Notificacion { get; set; }
        public Guid RolId { get; set; }
        public long UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
