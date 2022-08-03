using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Request;

namespace Service.MedicalRecord.Context.EntityConfiguration.Request
{
    public class RequestPackConfiguration : IEntityTypeConfiguration<RequestPack>
    {
        public void Configure(EntityTypeBuilder<RequestPack> builder)
        {
            builder.HasKey(x => new { x.SolicitudId, x.PaqueteId });

            builder
                .HasMany(x => x.Estudios)
                .WithOne(x => x.Paquete)
                .HasForeignKey(x => new { x.SolicitudId, x.PaqueteId })
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
