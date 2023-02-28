using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Loyalty;

namespace Service.Catalog.Context.EntityConfiguration.Branch
{
    public class BranchBudgetConfiguration : IEntityTypeConfiguration<BudgetBranch>
    {
        public void Configure(EntityTypeBuilder<BudgetBranch> builder)
        {
            builder.ToTable("Relacion_Presupuesto_Sucursal");
            builder.HasKey(x => x.Id);

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
              .Property(x => x.UsuarioModificoId)
              .IsRequired(false);

            builder
              .Property(x => x.FechaModifico)
              .IsRequired(false);
        }
    }
}
