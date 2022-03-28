using Identidad.Api.ViewModels.Menu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identidad.Api.Infraestructure.EntityConfiguration.CatalogoMedicosConfiguration
{
    public class MedicsConfiguration : IEntityTypeConfiguration<Medics>
    {
        public void Configure(EntityTypeBuilder<Medics> builder)
        {
            builder.ToTable("CAT_Medico");

            builder.HasKey(x => x.IdMedico);

            builder
              .Property(x => x.Clave)
              .IsRequired(true)
              .HasMaxLength(15);

            builder
              .Property(x => x.Nombre)
              .IsRequired(true)
              .HasMaxLength(100);

            builder
              .Property(x => x.Correo)
              .IsRequired(true)
              .HasMaxLength(15);

            builder
              .Property(x => x.Telefono)
              .IsRequired(true);

            builder
              .Property(x => x.Celular)
              .IsRequired(true);

            builder
              .Property(x => x.Calle)
              .IsRequired(true)
              .HasMaxLength(100);

            builder
              .Property(x => x.Clinicas)
              .IsRequired(true);

            builder
              .Property(x => x.Observaciones)
              .IsRequired(false)
              .HasMaxLength(255);

            builder
              .Property(x => x.EspecialidadId)
              .IsRequired(true);

            builder
              .Property(x => x.Activo)
              .IsRequired(true);

        }
    }
}
