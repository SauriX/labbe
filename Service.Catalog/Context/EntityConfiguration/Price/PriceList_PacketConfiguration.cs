using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Price;

namespace Service.Catalog.Context.EntityConfiguration.Price
{
    public class PriceList_PacketConfiguration : IEntityTypeConfiguration<PriceList_Packet>
    {
        public void Configure(EntityTypeBuilder<PriceList_Packet> builder)
        {
            builder.ToTable("Relacion_ListaP_Paquete");

            builder.HasKey(x => new { x.PrecioId, x.PaqueteId });

            builder
              .HasOne(x => x.Precio)
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
