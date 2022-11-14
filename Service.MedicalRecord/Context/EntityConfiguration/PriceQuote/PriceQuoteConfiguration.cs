using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.MedicalRecord.Context.EntityConfiguration.PriceQuote
{
    public class PriceQuoteConfiguration : IEntityTypeConfiguration<Service.MedicalRecord.Domain.PriceQuote.PriceQuote>
    {
        public void Configure(EntityTypeBuilder<Domain.PriceQuote.PriceQuote> builder)
        {
            builder.ToTable("CAT_Cotizaciones");
            builder.HasKey(x => x.Id);


            builder
                .Property(x => x.NombrePaciente)
                .IsRequired(true);

            builder
                .HasMany(x => x.Estudios)
                .WithOne(x => x.Cotizacion)
                .HasForeignKey(x => x.CotizacionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
