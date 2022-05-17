using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Promotion;

namespace Service.Catalog.Context.EntityConfiguration.Promotion
{
    public class PromotionLoyalityConfiguration : IEntityTypeConfiguration<PromotionLoyality>
    {
        public void Configure(EntityTypeBuilder<PromotionLoyality> builder)
        {
            builder.ToTable("Relacion_Promocion_Lealtad");

            builder.HasKey(x => new {
                x.PromotionId,
                x.LoyalityId
            });
        }
    }
}
