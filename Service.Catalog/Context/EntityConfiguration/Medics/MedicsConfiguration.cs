using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Catalog.Context.EntityConfiguration.Medics
{
    public class MedicsConfiguration : IEntityTypeConfiguration<Domain.Medics.Medics>
    {
        public void Configure(EntityTypeBuilder<Domain.Medics.Medics> builder)
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
              .IsRequired(false)
              .HasMaxLength(15);

            builder
              .Property(x => x.Calle)
              .IsRequired(false)
              .HasMaxLength(100);

            builder
              .Property(x => x.NumeroExterior)
              .IsRequired(false)
              .HasMaxLength(100);

            builder
              .Property(x => x.NumeroInterior)
              .IsRequired(false)
              .HasMaxLength(100);

            builder
              .Property(x => x.CiudadId)
              .IsRequired(false)
              .HasMaxLength(50);

            builder
              .Property(x => x.EstadoId)
              .IsRequired(false)
              .HasMaxLength(50);

            builder
              .Property(x => x.Telefono)
              .IsRequired(false);

            builder
              .Property(x => x.Celular)
              .IsRequired(false);

            builder
              .Property(x => x.Calle)
              .IsRequired(false)
              .HasMaxLength(100);

            builder
              .Property(x => x.Observaciones)
              .IsRequired(false)
              .HasMaxLength(255);

            builder
              .Property(x => x.Activo)
              .IsRequired(true);

            builder
                .HasOne(x => x.Colonia)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Especialidad)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(x => x.Clinicas)
                .WithOne(x => x.Medico)
                //.HasForeignKey(x => x.ClinicaId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
