using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Parameter;

namespace Service.Catalog.Context.EntityConfiguration.Parameter
{
    public class ParameterConfiguration : IEntityTypeConfiguration<Parameters>
    {
        public void Configure(EntityTypeBuilder<Parameters> builder)
        {
            builder.ToTable("CAT_Parametro");

            builder.HasKey(x => x.IdParametro);

            builder
              .Property(x => x.Clave)
              .IsRequired(false)
              .HasMaxLength(100);

            builder
                .Property(x => x.Nombre)
                .IsRequired(false)
                .HasMaxLength(100);

            builder
                .Property(x => x.ValorInicial)
                .IsRequired(true)
                .HasMaxLength(100);

            builder
                .Property(x=>x.NombreCorto)
                .IsRequired(true)
                .HasMaxLength(50);

            builder
                .Property(x => x.Unidades)
                .IsRequired(true);

            builder
                .Property(x => x.Formula)
                .IsRequired(false)
                .HasMaxLength(200);

            builder
                .Property(x => x.Formato)
                .IsRequired(true)
                .HasMaxLength(100);

            builder
                .HasOne(x => x.Area)
                .WithOne();

            builder
                .HasOne(x => x.Reagent)
                .WithOne();

            builder
                .Property(x => x.UnidadSi)
                .IsRequired(true)
                .HasMaxLength(50);

            builder
                .Property(x => x.FCSI)
                .IsRequired(true)
                .HasMaxLength(50);

            builder
                .HasOne(x => x.Department)
                .WithOne()
                .HasForeignKey<Parameters>(x=>x.DepartamentId);
            
        }
    }
}
