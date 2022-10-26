using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Status;

namespace Service.MedicalRecord.Context.EntityConfiguration.Status
{
    public class StatusRequestConfiguration : IEntityTypeConfiguration<StatusRequest>
    {
        public void Configure(EntityTypeBuilder<StatusRequest> builder)
        {
            builder.ToTable("Estatus_Solicitud");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();
        }
    }
}
