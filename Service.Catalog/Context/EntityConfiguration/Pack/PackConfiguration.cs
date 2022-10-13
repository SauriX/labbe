using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Packet;

namespace Service.Catalog.Context.EntityConfiguration.Pack
{
    public class PackConfiguration : IEntityTypeConfiguration<Packet>
    {
        public void Configure(EntityTypeBuilder<Packet> builder)
        {
            builder.ToTable("CAT_Paquete");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Clave)
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(x => x.Nombre)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.NombreLargo)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.Visibilidad)
                .IsRequired(true);

            builder.Property(x => x.Activo)
                .IsRequired(true);

            builder
                .Property(x => x.FechaCreo)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.FechaModifico)
                .HasColumnType("smalldatetime");
        }
    }
}
