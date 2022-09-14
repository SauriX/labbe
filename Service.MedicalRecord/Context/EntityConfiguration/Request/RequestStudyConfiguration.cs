using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.Request;
using System;

namespace Service.MedicalRecord.Context.EntityConfiguration.Request
{
    public class RequestStudyConfiguration : IEntityTypeConfiguration<RequestStudy>
    {
        public void Configure(EntityTypeBuilder<RequestStudy> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.FechaEntrega)
                .HasDefaultValueSql("getdate()")
                .HasColumnType("smalldatetime");
        }
    }
}
