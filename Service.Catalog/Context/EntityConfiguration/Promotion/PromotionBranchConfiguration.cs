using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Promotion;

namespace Service.Catalog.Context.EntityConfiguration.Promotion
{
    public class PromotionBranchConfiguration : IEntityTypeConfiguration<PromotionBranch>
    {
        public void Configure(EntityTypeBuilder<PromotionBranch> builder)
        {
            builder.ToTable("Relacion_Promocion_Sucursal");

            builder.HasKey(x => new
            {
                x.PromocionId,
                x.SucursalId
            });
        }
    }
}
