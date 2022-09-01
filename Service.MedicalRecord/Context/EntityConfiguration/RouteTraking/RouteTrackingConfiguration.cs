using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.RouteTracking;

namespace Service.MedicalRecord.Context.EntityConfiguration.RouteTraking
{
    public class RouteTrackingConfiguration : IEntityTypeConfiguration<Domain.RouteTracking.RouteTracking>
    {
        public void Configure(EntityTypeBuilder<RouteTracking> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Solicitud)
                .WithMany();
        }
    }
}
