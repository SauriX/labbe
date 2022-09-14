using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Request;

namespace Service.MedicalRecord.Context.EntityConfiguration.Request
{
    public class RequestPackConfiguration : IEntityTypeConfiguration<RequestPack>
    {
        public void Configure(EntityTypeBuilder<RequestPack> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasMany(x => x.Estudios)
                .WithOne(x => x.Paquete)
                .HasForeignKey(x => new { x.PaqueteId })
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
