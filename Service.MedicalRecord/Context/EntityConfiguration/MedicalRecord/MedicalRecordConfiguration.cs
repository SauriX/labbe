using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.MedicalRecord.Context.EntityConfiguration.MedicalRecord
{
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<Domain.MedicalRecord.MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<Domain.MedicalRecord.MedicalRecord> builder)
        {
            builder.ToTable("CAT_Expedientes");
            builder.HasKey(x => x.Id);
            builder
             .HasMany(x => x.TaxData)
             .WithOne(x => x.Expediente)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
