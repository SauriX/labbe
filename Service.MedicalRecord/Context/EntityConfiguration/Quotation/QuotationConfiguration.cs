using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.MedicalRecord.Context.EntityConfiguration.Quotation
{
    public class QuotationConfiguration : IEntityTypeConfiguration<Domain.Quotation.Quotation>
    {
        private const byte VIGENTE = 1;
        private const byte PARTICULAR = 2;
        private const byte URGENCIA_NORMAL = 1;

        public void Configure(EntityTypeBuilder<Domain.Quotation.Quotation> builder)
        {
            builder.ToTable("CAT_Cotizacion");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.EstatusId)
                .HasDefaultValue(VIGENTE);

            builder
                .Property(x => x.Procedencia)
                .HasDefaultValue(PARTICULAR);

            builder
                .HasMany(x => x.Estudios)
                .WithOne(x => x.Cotizacion)
                .HasForeignKey(x => x.CotizacionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(x => x.Paquetes)
                .WithOne(x => x.Cotizacion)
                .HasForeignKey(x => x.CotizacionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
