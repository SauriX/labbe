using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Domain.PriceQuote;

namespace Service.MedicalRecord.Context.EntityConfiguration.PriceQuote
{
    public class PriceQuotePackConfiguration : IEntityTypeConfiguration<PriceQuotePack>
    {
        public void Configure(EntityTypeBuilder<PriceQuotePack> builder)
        {
            builder.ToTable("Relacion_Cotizacion_Paquete");

            builder.HasKey(x => x.Id);

            builder
                .HasMany(x => x.Estudios)
                .WithOne(x => x.Paquete)
                .HasForeignKey(x => x.PaqueteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
