using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Catalog.Context.EntityConfiguration
{

    public class PriceConfiguration : IEntityTypeConfiguration<Domain.Price.PriceList>
    {
        public void Configure(EntityTypeBuilder<Domain.Price.PriceList> builder)
        {
            builder.ToTable("CAT_ListaPrecio");

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


            builder
             .HasMany(x => x.Compañia)
             .WithOne(x => x.PrecioLista)
             .OnDelete(DeleteBehavior.Restrict);

            builder
             .HasMany(x => x.Promocion)
             .WithOne(x => x.PrecioLista)
             .OnDelete(DeleteBehavior.Restrict);

            builder
             .HasMany(x => x.Estudios)
             .WithOne(x => x.PrecioLista)
             .OnDelete(DeleteBehavior.Restrict);

            builder
             .HasMany(x => x.Paquete)
             .WithOne(x => x.PrecioLista)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
