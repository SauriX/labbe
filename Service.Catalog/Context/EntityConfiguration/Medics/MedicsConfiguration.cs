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
              .Property(x => x.PrimerApellido)
              .IsRequired(true)
              .HasMaxLength(100);
            builder
              .Property(x => x.SegundoApellido)
              .IsRequired(false)
              .HasMaxLength(100);
            builder
              .Property(x => x.Correo)
              .IsRequired(false)
              .HasMaxLength(100);
            builder
              .Property(x => x.CodigoPostal)
              .IsRequired(true)
              .HasMaxLength(15);
            builder
              .Property(x => x.Calle)
              .IsRequired(true)
              .HasMaxLength(100);
            builder
              .Property(x => x.NumeroExterior)
              .IsRequired(true)
              .HasMaxLength(100);
            builder
              .Property(x => x.NumeroInterior)
              .IsRequired(false)
              .HasMaxLength(100);

            builder
              .Property(x => x.CiudadId)
              .IsRequired(false)
              .HasMaxLength(15);

            builder
              .Property(x => x.EstadoId)
              .IsRequired(false)
              .HasMaxLength(15);

            builder
              .Property(x => x.ColoniaId)
              .IsRequired(true)
              .HasMaxLength(15);

            builder
              .Property(x => x.Telefono)
              .IsRequired(false);

            builder
              .Property(x => x.Celular)
              .IsRequired(false);

            builder
              .Property(x => x.Calle)
              .IsRequired(true)
              .HasMaxLength(100);

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

            builder
                .HasMany(x => x.Clinicas)
                .WithOne(x => x.Medico)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
