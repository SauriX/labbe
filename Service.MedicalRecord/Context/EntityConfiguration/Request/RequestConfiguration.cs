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
            builder.ToTable("CAT_Solicitud");

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
                .HasMany(x => x.Pagos)
                .WithOne(x => x.Solicitud)
                .HasForeignKey(x => x.SolicitudId)
                .OnDelete(DeleteBehavior.Cascade);

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

            builder
                .HasMany(x => x.FacturasCompañia)
                .WithMany(x => x.Solicitudes)
                .UsingEntity<Domain.Invoice.RequestInvoiceCompany>(
                    rc => rc.HasOne(prop => prop.InvoiceCompany).WithMany().HasForeignKey(prop => prop.InvoiceCompanyId),
                    rc => rc.HasOne(prop => prop.Solicitud).WithMany().HasForeignKey(prop => prop.SolicitudId)
                );

            builder
                .HasMany(x => x.Etiquetas)
                .WithOne(x => x.Solicitud)
                .HasForeignKey(x => x.SolicitudId)
                .OnDelete(DeleteBehavior.Restrict);
                
        }
    }
}
