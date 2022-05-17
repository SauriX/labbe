using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Price;

namespace Service.Catalog.Context.EntityConfiguration.Price
{
    public class Price_BranchConfiguration : IEntityTypeConfiguration<Price_Branch>
    {
        public void Configure(EntityTypeBuilder<Price_Branch> builder)
        {
            builder.ToTable("CAT_ListaP_Sucursal");

            builder.HasKey(x => new { x.PrecioListaId, x.SucursalId });

            builder
              .HasOne(x => x.PrecioLista)
              .WithMany()
              .OnDelete(DeleteBehavior.Restrict);


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
