using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Report.Domain.Request;

namespace Service.Report.Context.EntityConfiguration
{
    public class RequestStudyConfiguration : IEntityTypeConfiguration<RequestStudy>
    {
        public void Configure(EntityTypeBuilder<RequestStudy> builder)
        {
            builder.HasKey(x => new { x.SolicitudId, x.Id });
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}

