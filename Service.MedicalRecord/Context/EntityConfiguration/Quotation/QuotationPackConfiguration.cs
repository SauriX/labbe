using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Domain.Quotation;

namespace Service.MedicalRecord.Context.EntityConfiguration.Quotation
{
    public class QuotationPackConfiguration : IEntityTypeConfiguration<QuotationPack>
    {
        public void Configure(EntityTypeBuilder<QuotationPack> builder)
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
