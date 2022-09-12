using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Loyalty;

namespace Service.Catalog.Context.EntityConfiguration.Loyalty
{
    public class LoyaltyPriceListConfiguration : IEntityTypeConfiguration<LoyaltyPriceList>
    {
        public void Configure(EntityTypeBuilder<LoyaltyPriceList> builder)
        {
            builder.ToTable("Relacion_Loyalty_PrecioLista");

            builder.HasKey(x => new
            {
                x.LoyaltyId,
                x.PrecioListaId
            });

            //builder.HasOne(x => x.PrecioLista)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}