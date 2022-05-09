using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Study;

namespace Service.Catalog.Context.EntityConfiguration.Study
{
    public class ReagentStudyConfiguration : IEntityTypeConfiguration<ReagentStudy>
    {
        public void Configure(EntityTypeBuilder<ReagentStudy> builder)
        {
            builder.ToTable("Relacion_Estudio_Reactivo");

            builder.HasKey(x => new {
                x.EstudioId,
                x.ReagentId
            });
        }
    }
}
