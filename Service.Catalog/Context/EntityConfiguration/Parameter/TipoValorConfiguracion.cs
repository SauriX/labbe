using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Parameter;

namespace Service.Catalog.Context.EntityConfiguration.Parameter
{
    public class TipoValorConfiguracion : IEntityTypeConfiguration<TipoValor>
    {
        public void Configure(EntityTypeBuilder<TipoValor> builder)
        {
            builder.ToTable("CAT_Tipo_Valor");
            builder.HasKey(x => x.IdTipo_Valor);

            builder
                .HasOne(x => x.parametro)
                .WithMany()
                .HasForeignKey(x=>x.IdParametro);
                

        }
    }
}
