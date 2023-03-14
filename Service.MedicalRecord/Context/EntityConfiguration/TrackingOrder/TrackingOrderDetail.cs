﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.MedicalRecord.Context.EntityConfiguration.TrackingOrder
{
    public class TrackingOrderDetail : IEntityTypeConfiguration<Domain.TrackingOrder.TrackingOrderDetail>
    {
        public void Configure(EntityTypeBuilder<Domain.TrackingOrder.TrackingOrderDetail> builder)
        {
            builder.HasOne(x => x.Etiqueta)
            .WithMany()
            .HasForeignKey(x => x.EtiquetaId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
