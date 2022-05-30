using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Catalog;

namespace Service.Catalog.Context.EntityConfiguration.Catalog
{
    public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.ToTable("CAT_Clinica");

            builder.HasKey(x => x.Id);

            builder
              .Property(x => x.Clave)
              .IsRequired(true)
              .HasMaxLength(15);

            builder
              .Property(x => x.Nombre)
              .IsRequired(true)
              .HasMaxLength(100);
            
            builder
              .Property(x => x.Activo)
              .IsRequired(true);

        }
    }
}
