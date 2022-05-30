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
                .Property(x => x.CantidadDescuento)
                .IsRequired(true)
                .HasMaxLength(100);

            builder
                .Property(x => x.FechaInicio)
                .IsRequired(true);

            builder
                .Property(x => x.FechaFinal)
                .IsRequired(true);

            builder
                .Property(x => x.Visibilidad)
                .IsRequired(true);
            builder
              .Property(x => x.Activo)
              .IsRequired(true);

            builder
                .HasMany(x => x.packs)
                .WithOne(x => x.Promotion);

            builder
                .HasMany(x => x.studies)
                .WithOne(x => x.Promotion);

            builder
                .HasMany(x => x.branches)
                .WithOne(x => x.Promotion);

            builder
                .HasMany(x => x.loyalities)
                .WithOne(x => x.Promotion);
        }
    }
}
