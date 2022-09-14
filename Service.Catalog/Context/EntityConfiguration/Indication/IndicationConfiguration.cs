using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Catalog.Context.EntityConfiguration.Indication
{

    public class IndicationConfiguration : IEntityTypeConfiguration<Domain.Indication.Indication>
    {
        public void Configure(EntityTypeBuilder<Domain.Indication.Indication> builder)
        {
            builder.ToTable("CAT_Indicacion");

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
              .Property(x => x.Descripcion)
              .IsRequired(false)
              .HasMaxLength(1000);

            builder
              .Property(x => x.FechaCreo)
              .HasColumnType("smalldatetime");

            builder
              .Property(x => x.FechaModifico)
              .HasColumnType("smalldatetime");

            builder
               .HasMany(x => x.Estudios)
               .WithOne(x => x.Indicacion)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
