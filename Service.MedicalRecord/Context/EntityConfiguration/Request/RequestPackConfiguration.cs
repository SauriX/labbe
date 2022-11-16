using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Request;

namespace Service.MedicalRecord.Context.EntityConfiguration.Request
{
    public class RequestPackConfiguration : IEntityTypeConfiguration<RequestPack>
    {
        public void Configure(EntityTypeBuilder<RequestPack> builder)
        {
            builder.ToTable("Relacion_Solicitud_Paquete");

            builder.HasKey(x => x.Id);

            builder
                .HasMany(x => x.Estudios)
                .WithOne(x => x.Paquete)
                .HasForeignKey(x =>  x.PaqueteId )
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
