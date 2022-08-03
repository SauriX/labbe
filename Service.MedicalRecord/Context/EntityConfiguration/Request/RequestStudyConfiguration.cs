using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Request;

namespace Service.MedicalRecord.Context.EntityConfiguration.Request
{
    public class RequestStudyConfiguration : IEntityTypeConfiguration<RequestStudy>
    {
        public void Configure(EntityTypeBuilder<RequestStudy> builder)
        {
            builder.HasKey(x => new { x.SolicitudId, x.EstudioId });
        }
    }
}
