using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Status;

namespace Service.MedicalRecord.Context.EntityConfiguration.Status
{
    public class StatusRequestPaymentConfiguration : IEntityTypeConfiguration<StatusRequestPayment>
    {
        public void Configure(EntityTypeBuilder<StatusRequestPayment> builder)
        {
            builder.ToTable("Estatus_Solicitud_Pago");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();
        }
    }
}
