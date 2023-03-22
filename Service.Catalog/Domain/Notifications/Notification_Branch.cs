using System;

namespace Service.Catalog.Domain.Notifications
{
    public class Notification_Branch
    {
        public Guid NotificacionId { get; set; }
        public virtual Notifications Notificacion { get; set; }
        public Guid BranchId { get; set; }
        public virtual Branch.Branch Branch { get; set; }
        public long UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
