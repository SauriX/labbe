using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Promotion;

namespace Service.Catalog.Context.EntityConfiguration.Promotion
{
    public class PromotionStudyConfiguration : IEntityTypeConfiguration<PromotionStudy>
    {
    
        public void Configure(EntityTypeBuilder<PromotionStudy> builder)
        {
            builder.ToTable("Relacion_Promocion_Estudio");

            builder.HasKey(x => new {
                x.PromotionId,
                x.StudyId,
            });
        }
    }
}
