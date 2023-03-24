using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Service.Catalog.Context.EntityConfiguration.Notifications
{
    public class NotificatiosConfiguration : IEntityTypeConfiguration<Domain.Notifications.Notifications>
    {



        public void Configure(EntityTypeBuilder<Domain.Notifications.Notifications> builder)
        {
            builder.ToTable("Cat_notificaciones");
            builder.HasKey(x => x.Id);

            builder
                .HasMany(x => x.Sucursales)
                .WithOne(x => x.Notificacion);

            builder
                .HasMany(x => x.Roles)
                .WithOne(x => x.Notificacion);
        }
    }
}
