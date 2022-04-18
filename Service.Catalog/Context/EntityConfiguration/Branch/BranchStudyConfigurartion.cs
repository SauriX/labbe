using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Branch;

namespace Service.Catalog.Context.EntityConfiguration.Branch
{
    public class BranchStudyConfigurartion
    {
        public void Configure(EntityTypeBuilder<BranchStudy> builder)
        {
            builder.ToTable("Relacion_Estudio_Sucursal");
            builder.HasKey(t => t.Id);
            builder.HasKey(x => new { x.EstudioId, x.BranchId });



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
              .IsRequired(false);

        }
    }
}
