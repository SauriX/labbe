using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Study;

namespace Service.Catalog.Context.EntityConfiguration.Study
{
    public class WorkListStudyConfiguration : IEntityTypeConfiguration<WorkListStudy>
    {
        public void Configure(EntityTypeBuilder<WorkListStudy> builder)
        {
            builder.ToTable("Relacion_Estudio_WorkList");

            builder.HasKey(x => new {
                x.EstudioId,
                x.WorkListId,
            });
        }
    }
}
