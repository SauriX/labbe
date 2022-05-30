using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Company;

namespace Service.Catalog.Context.EntityConfiguration.CompanyConfiguration
{
    public class ContactConfiguration
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("CAT_Contacto");

            builder.HasKey(x => x.Id);

            builder
              .Property(x => x.Nombre)
              .IsRequired(true)
              .HasMaxLength(100);

            builder
              .Property(x => x.Apellidos)
              .IsRequired(false)
              .HasMaxLength(100);
            builder
              .Property(x => x.Correo)
              .IsRequired(false)
              .HasMaxLength(100);
            builder
               .Property(x => x.Telefono)
               .IsRequired(false);
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
              .IsRequired(false);
        }
    }
}
