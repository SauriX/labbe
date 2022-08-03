using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Request;

namespace Service.MedicalRecord.Context.EntityConfiguration.Request
{
    public class RequestConfiguration : IEntityTypeConfiguration<Domain.Request.Request>
    {
        public void Configure(EntityTypeBuilder<Domain.Request.Request> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasMany(x => x.Estudios)
                .WithOne(x => x.Solicitud)
                .HasForeignKey(x => x.SolicitudId)
                .OnDelete(DeleteBehavior.Restrict);     
            
            builder
                .HasMany(x => x.Paquetes)
                .WithOne(x => x.Solicitud)
                .HasForeignKey(x => x.SolicitudId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
