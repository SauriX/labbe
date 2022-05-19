using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Price;

namespace Service.Catalog.Context.EntityConfiguration.Price
{
    public class Price_MedicsConfiguration : IEntityTypeConfiguration<Price_Medics>
    {
        public void Configure(EntityTypeBuilder<Price_Medics> builder)
        {
            builder.ToTable("CAT_ListaP_Medicos");

            builder.HasKey(x => new { x.PrecioListaId, x.MedicoId });

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
