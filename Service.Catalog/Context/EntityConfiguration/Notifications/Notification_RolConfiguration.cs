using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Domain.Notifications;

namespace Service.Catalog.Context.EntityConfiguration.Notifications
{
    public class Notification_RolConfiguration : IEntityTypeConfiguration<Notification_Role>
    {

        void IEntityTypeConfiguration<Notification_Role>.Configure(EntityTypeBuilder<Notification_Role> builder)
        {
            builder.ToTable("Relacion_Notificacion_Rol");

            builder.HasKey(x => new
            {
                x.RolId,
                x.NotificacionId,
            });
        }
    }
}
