using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.PriceQuote;
using Service.MedicalRecord.Domain.Request;

namespace Service.MedicalRecord.Context.EntityConfiguration.PriceQuote
{
    public class cotizacionEstudio : IEntityTypeConfiguration<PriceQuoteStudy>
    {
        public void Configure(EntityTypeBuilder<PriceQuoteStudy> builder)
        {
            builder.ToTable("Relacion_Cotizacion_Estudio");

            builder.HasKey(x => x.Id);
        }
    }
}
