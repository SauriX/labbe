using Service.Catalog.Domain.Indication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Catalog.Context.EntityConfiguration.IndicationConfiguration
{
   
    public class IndicationConfiguration : IEntityTypeConfiguration<Indication>
    {
        public void Configure(EntityTypeBuilder<Indication> builder)
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
              .HasMaxLength(500);

            builder
              .Property(x => x.Activo)
              .IsRequired(true);

            builder
              .Property(x => x.UsuarioCreoId)
              .IsRequired(false);

            builder
              .Property(x => x.FechaCreo)
              .IsRequired(true);

            builder
              .Property(x => x.UsuarioModificoId)
              .IsRequired(false);

            builder
              .Property(x => x.FechaModifico)
              .IsRequired(false);

            builder
               .HasMany(x => x.Estudios)
               .WithOne(x => x.Indicacion)
               //.HasForeignKey(x => x.ClinicaId)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
