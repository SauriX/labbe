using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.PriceQuote;

namespace Service.MedicalRecord.Context.EntityConfiguration.PriceQuote
{
    public class cotizacionEstudio : IEntityTypeConfiguration<PriceQuoteStudy>
    {
        public void Configure(EntityTypeBuilder<PriceQuoteStudy> builder)
        {
            builder.ToTable("cotizacionStudies");
            builder.HasKey(x => x.Id);


        }
    }
}
