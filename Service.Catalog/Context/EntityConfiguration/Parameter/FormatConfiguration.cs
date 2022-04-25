using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Parameter;

namespace Service.Catalog.Context.EntityConfiguration.Parameter
{
    public class FormatConfiguration : IEntityTypeConfiguration<Format>
    {
        public void Configure(EntityTypeBuilder<Format> builder)
        {
            builder.ToTable("Cat_Formato");
            builder.HasKey(x=>x.NombreFormato);
        }
    }
}
