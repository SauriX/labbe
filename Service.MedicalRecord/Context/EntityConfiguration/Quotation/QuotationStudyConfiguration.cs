using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Quotation;
using Service.MedicalRecord.Domain.Request;

namespace Service.MedicalRecord.Context.EntityConfiguration.Quotation
{
    public class QuotationStudyConfiguration : IEntityTypeConfiguration<QuotationStudy>
    {
        public void Configure(EntityTypeBuilder<QuotationStudy> builder)
        {
            builder.ToTable("Relacion_Cotizacion_Estudio");

            builder.HasKey(x => x.Id);
        }
    }
}
