using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Report.Domain.Catalogs;

namespace Service.Report.Context.EntityConfiguration.Catalogs
{
    public class MedicConfiguration : IEntityTypeConfiguration<Medic>
    {
        public void Configure(EntityTypeBuilder<Medic> builder)
        {
            builder.ToTable("CAT_Medico");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
