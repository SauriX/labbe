using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Report.Domain.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Context.EntityConfiguration
{
    public class RequestPackConfiguration : IEntityTypeConfiguration<RequestPack>
    {
        public void Configure(EntityTypeBuilder<RequestPack> builder)
        {
            builder.HasKey(x => new { x.SolicitudId, x.PaqueteId });

            builder
                .HasMany(x => x.Estudios)
                .WithOne(x => x.Paquete)
                .HasForeignKey(x => new { x.SolicitudId, x.PaqueteId })
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
