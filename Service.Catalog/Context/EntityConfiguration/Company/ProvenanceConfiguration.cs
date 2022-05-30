using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Provenance;

namespace Service.Catalog.Context.EntityConfiguration.Company
{
    public class ProvenanceConfiguration : IEntityTypeConfiguration<Provenance>
    {
        public void Configure(EntityTypeBuilder<Provenance> builder)
        {
            builder.ToTable("CAT_Procedencia");

            builder.HasKey(x => x.Id);

            builder
              .Property(x => x.Clave)
              .IsRequired(true)
              .HasMaxLength(100);
            //.NotEqual();

            builder
              .Property(x => x.Nombre)
              .IsRequired(true)
              .HasMaxLength(100);
            
        }
    }
}
