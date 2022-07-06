using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.PriceQuote;

namespace Service.MedicalRecord.Context.EntityConfiguration.PriceQuote
{
    public class cotizacionEstudio : IEntityTypeConfiguration<CotizacionStudy>
    {
        public void Configure(EntityTypeBuilder<CotizacionStudy> builder)
        {
            builder.ToTable("cotizacionStudies");
            builder.HasKey(x=>x.CotizacionId);


        }
    }
}
