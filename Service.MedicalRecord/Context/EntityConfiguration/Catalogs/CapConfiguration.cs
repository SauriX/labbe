using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Catalogs;

namespace Service.MedicalRecord.Context.EntityConfiguration.Catalogs
{
    public class CapConfiguration : IEntityTypeConfiguration<Cap>
    {
        public void Configure(EntityTypeBuilder<Cap> builder)
        {
            builder.ToTable("CAT_Tipo_Tapon");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
