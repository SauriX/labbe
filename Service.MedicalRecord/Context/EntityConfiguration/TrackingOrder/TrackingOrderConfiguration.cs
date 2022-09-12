using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.MedicalRecord.Context.EntityConfiguration.TrackingOrder
{
    public class TrackingOrderConfiguration : IEntityTypeConfiguration<Domain.TrackingOrder.TrackingOrder>
    {
        public void Configure(EntityTypeBuilder<Domain.TrackingOrder.TrackingOrder> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Estudios)
                .WithOne(x => x.Seguimiento);

            builder.Navigation(x => x.Estudios);


        }
    }
}
