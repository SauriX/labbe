using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Price;

namespace Service.Catalog.Context.EntityConfiguration.Price
{
    public class PriceList_StudyConfiguration : IEntityTypeConfiguration<PriceList_Study>
    {
        public void Configure(EntityTypeBuilder<PriceList_Study> builder)
        {
            builder.ToTable("Relacion_ListaP_Estudio");

            builder.HasKey(x => new { x.PrecioListaId, x.EstudioId });

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
