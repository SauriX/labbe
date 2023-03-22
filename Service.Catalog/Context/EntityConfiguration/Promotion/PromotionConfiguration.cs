using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Catalog.Context.EntityConfiguration.Promotion
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Domain.Promotion.Promotion>
    {
        public void Configure(EntityTypeBuilder<Domain.Promotion.Promotion> builder)
        {
            builder.ToTable("CAT_Promocion");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Clave)
                .IsRequired(false)
                .HasMaxLength(100);

            builder
                .Property(x => x.Nombre)
                .IsRequired(true)
                .HasMaxLength(100);

            builder
                .Property(x => x.TipoDeDescuento)
                .IsRequired(true);

            builder
                .Property(x => x.Cantidad)
                .HasMaxLength(100);

            builder
              .Property(x => x.Activo)
              .IsRequired(true);

            builder
                .HasMany(x => x.Paquetes)
                .WithOne(x => x.Promocion);

            builder
                .HasMany(x => x.Estudios)
                .WithOne(x => x.Promocion);

            builder
                .HasMany(x => x.Sucursales)
                .WithOne(x => x.Promocion);

            builder
                .HasMany(x => x.Medicos)
                .WithOne(x => x.Promocion);

        }
    }
}
