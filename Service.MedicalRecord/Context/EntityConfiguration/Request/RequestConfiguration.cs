using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Request;

namespace Service.MedicalRecord.Context.EntityConfiguration.Request
{
    public class RequestConfiguration : IEntityTypeConfiguration<Domain.Request.Request>
    {
        private const byte VIGENTE = 1;
        private const byte PARTICULAR = 2;
        private const byte URGENCIA_NORMAL = 1;

        public void Configure(EntityTypeBuilder<Domain.Request.Request> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.EstatusId)
                .HasDefaultValue(VIGENTE);

            builder
                .Property(x => x.Procedencia)
                .HasDefaultValue(PARTICULAR);

            builder
                .Property(x => x.Urgencia)
                .HasDefaultValue(URGENCIA_NORMAL);

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

            builder
                .HasMany(x => x.Imagenes)
                .WithOne(x => x.Solicitud)
                .HasForeignKey(x => x.SolicitudId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
