using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Promotion;

namespace Service.Catalog.Context.EntityConfiguration.Promotion
{
    public class PromotionMedicConfiguration : IEntityTypeConfiguration<PromotionMedic>
    {
        public void Configure(EntityTypeBuilder<PromotionMedic> builder)
        {
            builder.ToTable("Relacion_Promocion_Medicos");

            builder.HasKey(x => new
            {
                x.PromocionId,
                x.MedicoId
            });
        }
    }
}
