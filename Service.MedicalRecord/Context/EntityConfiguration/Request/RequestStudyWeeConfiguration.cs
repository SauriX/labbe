using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Request;
using System;

namespace Service.MedicalRecord.Context.EntityConfiguration.Request
{
    public class RequestStudyWeeConfiguration : IEntityTypeConfiguration<RequestStudyWee>
    {
        public void Configure(EntityTypeBuilder<RequestStudyWee> builder)
        {
            builder.ToTable("Relacion_Estudio_WeeClinic");

            builder.HasKey(x => x.Id);
        }
    }
}
