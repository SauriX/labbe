using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Catalog.Context.EntityConfiguration.Route
{
    public class RouteConfiguration : IEntityTypeConfiguration<Domain.Route.Route>
    {
        public void Configure(EntityTypeBuilder<Domain.Route.Route> builder)
        {
            builder.ToTable("CAT_Rutas");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Clave)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.FechaCreo)
                .IsRequired(false)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.FechaModifico)
                .IsRequired(false)
                .HasColumnType("smalldatetime");
            builder
                .HasMany(x => x.Estudios)
                .WithOne(x => x.Ruta);

            builder
               .HasOne(x => x.Origen)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(x => x.Destino)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(x => x.Paqueteria)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);
            builder
               .HasOne(x => x.Maquilador)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
