using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Company;

namespace Service.Catalog.Context.EntityConfiguration.Company
{

    public class Price_CompanyConfiguration : IEntityTypeConfiguration<Price_Company>
    {
        public void Configure(EntityTypeBuilder<Price_Company> builder)
        {
            builder.ToTable("CAT_ListaP_Compañia");

            builder.HasKey(x => new { x.PrecioId, x.CompañiaId });
            
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
