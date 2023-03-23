using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Domain.Route;
using Service.Catalog.Domain.Notifications;

namespace Service.Catalog.Context.EntityConfiguration.Notifications
{
    public class Notification_branchConfiguration : IEntityTypeConfiguration<Notification_Branch>
    {

        void IEntityTypeConfiguration<Notification_Branch>.Configure(EntityTypeBuilder<Notification_Branch> builder)
        {
            builder.ToTable("Relacion_Notificacion_Sucursal");

            builder.HasKey(x => new
            {
                x.BranchId,
                x.NotificacionId,
            });
        }
    }
}
