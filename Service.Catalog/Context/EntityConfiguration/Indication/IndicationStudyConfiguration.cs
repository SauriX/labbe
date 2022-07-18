using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Indication;

namespace Service.Catalog.Context.EntityConfiguration.Indication
{
    public class IndicationStudyConfiguration : IEntityTypeConfiguration<IndicationStudy>
    {
        public void Configure(EntityTypeBuilder<IndicationStudy> builder)
        {
            builder.ToTable("Relacion_Estudio_Indicacion");

            builder.HasKey(x => new { x.EstudioId, x.IndicacionId });

            builder
              .Property(x => x.FechaCreo)
              .HasColumnType("smalldatetime");

            builder
              .Property(x => x.FechaMod)
              .HasColumnType("smalldatetime");
        }
    }
}
