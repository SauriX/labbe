﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain.Medics;

namespace Service.Catalog.Context.EntityConfiguration.Medics
{
    public class MedicClinicConfiguration : IEntityTypeConfiguration<MedicClinic>
    {
        public void Configure(EntityTypeBuilder<MedicClinic> builder)
        {
            builder.ToTable("CAT_Medico_Clinica");

            builder.HasKey(x => new { x.MedicoId, x.ClinicaId });



            builder
              .Property(x => x.Activo)
              .IsRequired(true);

            builder
              .Property(x => x.UsuarioCreoId)
              .IsRequired(true);

            builder
              .Property(x => x.FechaCreo)
              .IsRequired(true);

            builder
              .Property(x => x.UsuarioModId)
              .IsRequired(false);

            builder
              .Property(x => x.FechaMod)
              .IsRequired(false);

        }

    }
}
