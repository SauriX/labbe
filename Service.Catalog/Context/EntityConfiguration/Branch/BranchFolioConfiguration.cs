using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Branch;

namespace Service.Catalog.Context.EntityConfiguration.Branch
{
    public class BranchFolioConfiguration : IEntityTypeConfiguration<BranchFolioConfig>
    {
        public void Configure(EntityTypeBuilder<BranchFolioConfig> builder)
        {
            builder.ToTable("CAT_Sucursal_Folio");

            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.Ciudad)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Estado)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
