using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Catalog.Context.EntityConfiguration.Configuration
{
    public class TaxConfigurationConfiguration : IEntityTypeConfiguration<Domain.Configuration.TaxConfiguration>
    {
        public void Configure(EntityTypeBuilder<Domain.Configuration.TaxConfiguration> builder)
        {
            builder
                .HasKey(x => x.Id);

        }
    }
}
