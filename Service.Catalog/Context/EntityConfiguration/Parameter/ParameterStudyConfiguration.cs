using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Parameter;

namespace Service.Catalog.Context.EntityConfiguration.Parameter
{
    public class ParameterStudyConfiguration : IEntityTypeConfiguration<ParameterStudy>
    {
        public void Configure(EntityTypeBuilder<ParameterStudy> builder)
        {
            builder.ToTable("Relacion_Estudio_Parametro");

            builder.HasKey(x => x.Id);

            builder
              .Property(x => x.Activo)
              .IsRequired(true);

            builder
                .Property(x => x.FechaCreo)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.FechaModifico)
                .HasColumnType("smalldatetime");

        }
    }
}

