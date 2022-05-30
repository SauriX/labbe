using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Catalog.Context.EntityConfiguration.Tapon
{
    public class TaponConfiguration : IEntityTypeConfiguration<Domain.Tapon.Tapon>
    {
        public void Configure(EntityTypeBuilder<Domain.Tapon.Tapon> builder)
        {
            builder.ToTable("CAT_Tipo_Tapon");

            builder.HasKey(x => x.Id);
        }
    }
}
