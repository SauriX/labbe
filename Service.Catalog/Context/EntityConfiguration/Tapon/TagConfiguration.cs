using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Tapon;

namespace Service.Catalog.Context.EntityConfiguration.Tapon
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("CAT_Etiqueta");

            builder.HasKey(x => x.Id);
        }
    }
}
