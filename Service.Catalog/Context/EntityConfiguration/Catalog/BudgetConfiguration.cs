using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Catalog;

namespace Service.Catalog.Context.EntityConfiguration.Catalog
{
    public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.ToTable("CAT_Presupuestos");
            builder.HasKey(x => x.Id);
            
            builder
                .Property(x => x.CostoFijo)
                .HasColumnType("decimal(18,2)");
            
            builder
                .HasMany(x => x.Sucursales)
                .WithOne(x => x.CostoFijo)
                .HasForeignKey(x => x.CostoFijoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
