using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Report.Domain.Catalogs;

namespace Service.Report.Context.EntityConfiguration.Catalogs
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("CAT_Sucursal");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
