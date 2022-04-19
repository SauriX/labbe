﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Catalog.Domain;
using Service.Catalog.Domain.Company;

namespace Service.Catalog.Context.EntityConfiguration.CompanyConfiguration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("CAT_Compañia");

            builder.HasKey(x => x.Id);

            builder
              .Property(x => x.Clave)
              .IsRequired(true)
              .HasMaxLength(100);

            builder
              .Property(x => x.Contrasena)
              .IsRequired(true)
              .HasMaxLength(100);
            builder
              .Property(x => x.NombreComercial)
              .IsRequired(true)
              .HasMaxLength(100);
            builder
             .Property(x => x.Procedencia)
             .IsRequired(true);
            builder
             .Property(x => x.ListaPrecioId)
             .IsRequired(false);
            builder
             .Property(x => x.PromocionesId)
             .IsRequired(false);
            builder
             .Property(x => x.ListaPrecioId)
             .IsRequired(false);
            builder
             .Property(x => x.CodigoPostal)
             .IsRequired(false);
            builder
             .Property(x => x.EstadoId)
             .IsRequired(false);
            builder
             .Property(x => x.FormaDePagoId)
             .IsRequired(false);
            builder
             .Property(x => x.DiasCredito)
             .IsRequired(false);
            builder
             .Property(x => x.CFDIId)
             .IsRequired(false);
            builder
             .Property(x => x.BancoId)
             .IsRequired(false);

            builder
              .Property(x => x.RFC)
              .IsRequired(true)
              .HasMaxLength(100);
            builder
              .Property(x => x.MunicipioId)
              .IsRequired(false);
            builder
              .Property(x => x.MetodoDePagoId)
              .IsRequired(true);

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

            builder
               .HasMany(x => x.Contacts)
               .WithOne(x => x.Compañia)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
