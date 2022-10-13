using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Study;

namespace Service.Catalog.Context.EntityConfiguration.Study
{
    public class PacketStudyConfiguration : IEntityTypeConfiguration<PacketStudy>
    {
        public void Configure(EntityTypeBuilder<PacketStudy> builder)
        {
            builder.ToTable("Relacion_Estudio_Paquete");

            builder.HasKey(x => new
            {
                x.EstudioId,
                x.PacketId
            });

            builder
                .Property(x => x.FechaCreo)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.FechaModifico)
                .HasColumnType("smalldatetime");
        }
    }
}
