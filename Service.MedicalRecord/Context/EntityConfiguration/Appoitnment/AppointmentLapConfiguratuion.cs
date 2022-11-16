using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Appointments;

namespace Service.MedicalRecord.Context.EntityConfiguration.Appoitnment
{
    public class AppointmentLapConfiguratuion : IEntityTypeConfiguration<AppointmentLab>
    {
        public void Configure(EntityTypeBuilder<AppointmentLab> builder)
        {
            builder.ToTable("CAT_Cita_Lab");
            builder.HasKey(x => x.Id);
            //builder
            //    .HasMany(x => x.Estudios)
            //    .WithOne();

        }
    }
}
