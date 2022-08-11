using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Request;

namespace Service.MedicalRecord.Context.EntityConfiguration.Request
{
    public class RequestStudyStatusConfiguration : IEntityTypeConfiguration<RequestStudyStatus>
    {
        public void Configure(EntityTypeBuilder<RequestStudyStatus> builder)
        {
            builder.ToTable("Estatus_Solicitud_Estudio");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();
        }
    }
}
