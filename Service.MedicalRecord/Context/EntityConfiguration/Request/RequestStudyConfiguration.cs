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

            builder
                .Property(x => x.FechaTomaMuestra)
                .IsRequired(false)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.FechaValidacion)
                .IsRequired(false)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.FechaSolicitado)
                .IsRequired(false)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.FechaCaptura)
                .IsRequired(false)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.FechaLiberado)
                .IsRequired(false)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.FechaEnviado)
                .IsRequired(false)
                .HasColumnType("smalldatetime");

            builder
                .HasMany(x => x.Resultados)
                .WithOne(x => x.SolicitudEstudio)
                .HasForeignKey(x => x.SolicitudEstudioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(x => x.ResultadosPatologicos)
                .WithOne(x => x.SolicitudEstudio)
                .HasForeignKey(x => x.SolicitudEstudioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
