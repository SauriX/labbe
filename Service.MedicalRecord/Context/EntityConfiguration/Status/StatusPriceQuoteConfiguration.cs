using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Status;

namespace Service.MedicalRecord.Context.EntityConfiguration.Status
{
    public class StatusPriceQuoteConfiguration : IEntityTypeConfiguration<StatusPriceQuote>
    {
        public void Configure(EntityTypeBuilder<StatusPriceQuote> builder)
        {
            builder.ToTable("Estatus_Cotizacion");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();
        }
    }
}
