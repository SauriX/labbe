using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Report.Domain.Request;

namespace Service.Report.Context.EntityConfiguration
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasKey(x => x.SolicitudId);
            builder.Property(x => x.SolicitudId).ValueGeneratedNever();
            builder
                .HasMany(x => x.Estudios)
                .WithOne(x => x.Solicitud)
                .HasForeignKey(x => x.SolicitudId);
            builder
                .HasMany(x => x.MetodoPago)
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
