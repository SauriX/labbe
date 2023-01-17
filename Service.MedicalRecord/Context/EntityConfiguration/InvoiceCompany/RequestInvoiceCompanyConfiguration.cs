using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Invoice;

namespace Service.MedicalRecord.Context.EntityConfiguration.InvoiceCompany
{
    public class RequestInvoiceCompanyConfiguration : IEntityTypeConfiguration<Domain.Invoice.RequestInvoiceCompany>
    {
        public void Configure(EntityTypeBuilder<RequestInvoiceCompany> builder)
        {
            builder.ToTable("Relacion_Solicitud_Factura_Compania");

            builder.HasKey(x => new { x.SolicitudId, x.InvoiceCompanyId });

            builder.Property(x => x.FechaCreo).HasDefaultValueSql("GETUTCDATE()");

        }
    }
}
