using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Price;

namespace Service.Catalog.Context.EntityConfiguration.Company
{
  
    public class Price_PromotionConfiguration : IEntityTypeConfiguration<Price_Promotion>
    {
        public void Configure(EntityTypeBuilder<Price_Promotion> builder)
        {
            builder.ToTable("CAT_ListaP_Promocion");

            builder.HasKey(x => new { x.PrecioListaId, x.PromocionId });

            //builder
            //  .HasOne(x => x.PrecioLista)
            //  .WithMany()
            //  .OnDelete(DeleteBehavior.Restrict);


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
