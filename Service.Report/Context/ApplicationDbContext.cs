using Microsoft.EntityFrameworkCore;
using Service.Report.Domain.Catalogs;
using Service.Report.Domain.Indicators;
using Service.Report.Domain.MedicalRecord;
using Service.Report.Domain.Request;
using System.Reflection;

namespace Service.Report.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<MedicalRecord> CAT_Expedientes { get; set; }
        public DbSet<Company> CAT_Compañia { get; set; }
        public DbSet<Request> CAT_Solicitud { get; set; }
        public DbSet<RequestPayment> CAT_Corte_Caja { get; set;}
        public DbSet<RequestStudy> Relación_Solicitud_Estudio { get; set; }
        public DbSet<Indicators> CAT_Indicadores { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
