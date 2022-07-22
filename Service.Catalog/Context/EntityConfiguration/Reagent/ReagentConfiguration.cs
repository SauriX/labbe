using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Catalog.Context.EntityConfiguration.Reagent
{
    public class ReagentConfiguration : IEntityTypeConfiguration<Domain.Reagent.Reagent>
    {
        public void Configure(EntityTypeBuilder<Domain.Reagent.Reagent> builder)
        {
            builder.ToTable("CAT_Reactivo_Contpaq");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Clave)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.ClaveSistema)
                .IsRequired(false)
                .HasMaxLength(100);

            builder
                .Property(x => x.NombreSistema)
                .IsRequired(false)
                .HasMaxLength(100);

            builder
                .Property(x => x.FechaCreo)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.FechaModifico)
                .HasColumnType("smalldatetime");
        }
    }
}
