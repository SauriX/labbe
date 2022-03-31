using Identidad.Api.Model.Medicos;
using Identidad.Api.ViewModels.Menu;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Domain.Reagent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Service.Catalog.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Reagent> CAT_Reactivo_Contpaq { get; set; }
        public DbSet<Medics> CAT_Medicos { get; set; }
        public DbSet<MedicClinic> CAT_Medicos_Clinica { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
