using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Parameter;

namespace Service.Catalog.Context.EntityConfiguration.Parameter
{
    public class ParameterReagentConfiguration : IEntityTypeConfiguration<ParameterReagent>
    {
        public void Configure(EntityTypeBuilder<ParameterReagent> builder)
        {
            builder.ToTable("Relacion_Reactivo_Parametro");

            builder.HasKey(x => new { x.ReactivoId, x.ParametroId });

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
