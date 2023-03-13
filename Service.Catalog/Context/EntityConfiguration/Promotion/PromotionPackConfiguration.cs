using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Promotion;

namespace Service.Catalog.Context.EntityConfiguration.Promotion
{
    public class PromotionPackConfiguration : IEntityTypeConfiguration<PromotionPack>
    {
        public void Configure(EntityTypeBuilder<PromotionPack> builder)
        {
            builder.ToTable("Relacion_Promocion_Paquete");

            builder.HasKey(x => new
            {
                x.PromocionId,
                x.PaqueteId
            });
        }
    }
}
