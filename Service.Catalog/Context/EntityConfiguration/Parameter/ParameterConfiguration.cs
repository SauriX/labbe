using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Parameter;

namespace Service.Catalog.Context.EntityConfiguration.Parameter
{
    public class ParameterConfiguration : IEntityTypeConfiguration<Domain.Parameter.Parameter>
    {
        public void Configure(EntityTypeBuilder<Domain.Parameter.Parameter> builder)
        {
            builder.ToTable("CAT_Parametro");

            builder.HasKey(x => x.Id);

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

            /*builder
                .Property(x => x.Unidades)
                .IsRequired(true);*/

            builder
                .Property(x => x.Formula)
                .IsRequired(false)
                .HasMaxLength(200);

            builder
                .Property(x => x.TipoValor)
                .HasDefaultValue(0)
                .HasMaxLength(100);

            builder
                .HasOne(x=>x.Area)
                .WithMany();

            builder
                .HasOne(x => x.Reactivo)
                .WithMany();

            /*builder
                .Property(x => x.UnidadSi)
                .IsRequired(true)
                .HasMaxLength(50);*/

            builder
                .Property(x => x.FCSI)
                .IsRequired(true)
                .HasMaxLength(50);

            builder
               .HasMany(x => x.Estudios)
               .WithOne (x=>x.Parametro)
               .HasForeignKey(x=>x.ParametroId)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
