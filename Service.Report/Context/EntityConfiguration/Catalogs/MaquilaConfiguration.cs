using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Report.Domain.Catalogs;

namespace Service.Report.Context.EntityConfiguration.Catalogs
{
    public class MaquilaConfiguration : IEntityTypeConfiguration<Maquila>
    {
        public void Configure(EntityTypeBuilder<Maquila> builder)
        {
            builder.ToTable("CAT_Maquila");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
