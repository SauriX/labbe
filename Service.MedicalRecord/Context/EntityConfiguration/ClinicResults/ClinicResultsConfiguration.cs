using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Context.EntityConfiguration.ClinicResults
{
    public class ClinicResultsConfiguration : IEntityTypeConfiguration<Domain.ClinicResults>
    {
        public void Configure(EntityTypeBuilder<Domain.ClinicResults> builder)
        {
            builder.HasKey(x => x.Id);

            //builder
            //    .HasOne(x => x.SolicitudEstudio)
            //    .WithMany()
            //    .HasForeignKey(x => x.SolicitudEstudioId);

            builder
               .HasOne(x => x.Solicitud)
               .WithMany()
               .HasForeignKey(x => x.SolicitudId);
        }
    }
}
