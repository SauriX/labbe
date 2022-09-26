using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Promotion;

namespace Service.Catalog.Context.EntityConfiguration.Promotion
{
    public class PromotionMedicsConfiguration : IEntityTypeConfiguration<PromotionMedics>
    {


        public void Configure(EntityTypeBuilder<PromotionMedics> builder)
        {
            builder.ToTable("Relacion_Promocion_Medicos");

            builder.HasKey(x => new
            {
                x.PromotionId,
                x.MedicId
            });
        }
    }
}
