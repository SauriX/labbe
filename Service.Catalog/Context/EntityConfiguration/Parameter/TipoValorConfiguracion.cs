using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Parameter;

namespace Service.Catalog.Context.EntityConfiguration.Parameter
{
    public class TipoValorConfiguracion : IEntityTypeConfiguration<ParameterValue>
    {
        public void Configure(EntityTypeBuilder<ParameterValue> builder)
        {
            builder.ToTable("CAT_Tipo_Valor");
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.Parametro)
                .WithMany()
                .HasForeignKey(x=>x.ParametroId);
                

        }
    }
}
