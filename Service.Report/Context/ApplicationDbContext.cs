﻿using Microsoft.EntityFrameworkCore;
using Service.Report.Domain.MedicalRecord;
using Service.Report.Domain.Request;
using Service.Report.Domain.PatientStats;
using System.Reflection;

namespace Service.Catalog.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<MedicalRecord> MedicalRecord { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<PatientStats> PatientStats { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
