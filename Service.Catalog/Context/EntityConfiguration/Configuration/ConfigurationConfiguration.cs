using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Catalog.Context.EntityConfiguration.Configuration
{
    public class ConfigurationConfiguration : IEntityTypeConfiguration<Domain.Configuration.Configuration>
    {
        public void Configure(EntityTypeBuilder<Domain.Configuration.Configuration> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Valor)
                .IsRequired(false)
                .HasMaxLength(4000);

            builder
                .Property(x => x.Descripcion)
                .IsRequired()
                .HasMaxLength(4000);
        }
    }
}
