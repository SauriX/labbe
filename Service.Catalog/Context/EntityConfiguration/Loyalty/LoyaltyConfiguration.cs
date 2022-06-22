using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Catalog.Context.EntityConfiguration.Loyalty
{
    public class LoyaltyConfiguration : IEntityTypeConfiguration<Domain.Loyalty.Loyalty>
    {
        public void Configure(EntityTypeBuilder<Domain.Loyalty.Loyalty> builder)
        {
            builder.ToTable("CAT_Lealtad");

            builder.HasKey(x => x.Id);

            builder
              .Property(x => x.Clave)
              .IsRequired(true)
              .HasMaxLength(15);

            builder
              .Property(x => x.Nombre)
              .IsRequired(true)
              .HasMaxLength(50);

            builder
                .Property(x => x.CantidadDescuento)
                .IsRequired(true);

            builder
                .Property(x => x.FechaInicial)
                .IsRequired(true);

            builder
                .Property(x => x.FechaFinal)
                .IsRequired(true);
            builder
              .Property(x => x.Activo)
              .IsRequired(true);


            builder
              .Property(x => x.FechaCreo)
              .HasColumnType("date");

            builder
              .Property(x => x.FechaMod)
              .HasColumnType("date");

            builder
                .HasOne(x => x.PrecioLista)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
