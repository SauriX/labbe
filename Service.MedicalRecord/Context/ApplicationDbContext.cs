﻿using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Domain.PriceQuote;
using Service.MedicalRecord.Domain.TaxData;
using Shared.Error;
using System.Reflection;

namespace Service.MedicalRecord.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<MedicalRecord.Domain.MedicalRecord.MedicalRecord> CAT_Expedientes { get; set; }
        public DbSet<TaxData> CAT_Datos_Fiscales { get; set; }

        public DbSet<PriceQuote> CAT_Cotizaciones { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
