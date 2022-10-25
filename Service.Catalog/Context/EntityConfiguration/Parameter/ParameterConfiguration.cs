using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .HasMaxLength(4000);

            builder
                .Property(x => x.NombreCorto)
                .IsRequired(false)
                .HasMaxLength(4000);

            builder
                .Property(x => x.FechaCreo)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.FechaModifico)
                .HasColumnType("smalldatetime");

            builder
                .HasOne(x => x.Unidad)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);  
            
            builder
                .HasOne(x => x.UnidadSi)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(x => x.Formula)
                .IsRequired(false)
                .HasMaxLength(200);

            builder
                .Property(x => x.TipoValor)
                .IsRequired(false)
                .HasMaxLength(100);

            builder
                .HasOne(x => x.Area)
                .WithMany();

            builder
                .HasMany(x => x.Reactivos)
                .WithOne(x => x.Parametro)
                .HasForeignKey(x => x.ParametroId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(x => x.FCSI)
                .IsRequired(true)
                .HasMaxLength(50);

            builder
               .HasMany(x => x.Estudios)
               .WithOne(x => x.Parametro)
               .HasForeignKey(x => x.ParametroId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasMany(x => x.TipoValores)
               .WithOne(x => x.Parametro)
               .HasForeignKey(x => x.ParametroId);

        }
    }
}
