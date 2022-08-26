using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.EquipmentMantain;

namespace Service.Catalog.Context.EntityConfiguration.EqupmentMantain
{
    public class MantainConfiguration : IEntityTypeConfiguration<Mantain>
    {
        public void Configure(EntityTypeBuilder<Mantain> builder)
        {
            builder.ToTable("CAT_Mantenimiento_Equipo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Fecha_Prog)
                .IsRequired(true);
            builder.Property(x => x.Num_Serie)
                .IsRequired(true);

            builder.HasOne(x => x.Equipo)
              .WithMany();

            builder
               .HasMany(x => x.images)
               .WithOne(x=>x.Mantain)
               //.HasForeignKey(x=>x.MantainId)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
