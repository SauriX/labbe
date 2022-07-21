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

            builder.HasKey(x => new { x.EstudioId, x.ParametroId });



            builder
              .Property(x => x.Activo)
              .IsRequired(true);

            builder
              .Property(x => x.UsuarioCreoId)
              .IsRequired(true);

            builder
              .Property(x => x.FechaCreo)
              .IsRequired(true);

            builder
              .Property(x => x.UsuarioModId)
              .IsRequired(false);

            builder
              .Property(x => x.FechaMod)
              .IsRequired(true);

        }
    }
}

