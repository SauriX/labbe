using Identidad.Api.Model.Medicos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Indication;

namespace Service.Catalog.Context.EntityConfiguration.Medics
{
    public class IndicationStudyConfiguration : IEntityTypeConfiguration<IndicationStudy>
    {
        public void Configure(EntityTypeBuilder<IndicationStudy> builder)
        {
            builder.ToTable("Relacion_Estudio_Indicacion");

            builder.HasKey(x => new { x.EstudioId, x.IndicacionId});

                

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
