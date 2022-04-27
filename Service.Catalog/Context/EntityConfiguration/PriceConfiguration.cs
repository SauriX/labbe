using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Catalog.Context.EntityConfiguration
{

    public class PriceConfiguration : IEntityTypeConfiguration<Domain.Price.Price>
    {
        public void Configure(EntityTypeBuilder<Domain.Price.Price> builder)
        {
            builder.ToTable("CAT_ListPrecio");

            builder.HasKey(x => x.Id);

            builder
              .Property(x => x.Clave)
              .IsRequired(true)
              .HasMaxLength(100);

            builder
              .Property(x => x.Nombre)
              .IsRequired(true)
              .HasMaxLength(100);

            builder
              .Property(x => x.Visibilidad)
              .IsRequired(true)
              .HasMaxLength(100);

            //builder
            // .HasMany(x => x.Compañia)
            // .WithOne(x => x.Precio)
            // .HasForeignKey(x => x.PrecioId)
            // .OnDelete(DeleteBehavior.Restrict);

            //builder
            //   .HasOne(x => x.Procedencia)
            //   .WithMany()
            //   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
