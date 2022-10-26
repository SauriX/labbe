using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Status;

namespace Service.MedicalRecord.Context.EntityConfiguration.Status
{
    public class StatusRequestStudyConfiguration : IEntityTypeConfiguration<StatusRequestStudy>
    {
        public void Configure(EntityTypeBuilder<StatusRequestStudy> builder)
        {
            builder.ToTable("Estatus_Solicitud_Estudio");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();
        }
    }
}
