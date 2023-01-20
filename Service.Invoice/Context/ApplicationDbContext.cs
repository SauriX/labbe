using Microsoft.EntityFrameworkCore;
using Service.Billing.Domain.Invoice;
using Service.Billing.Domain.Series;
using System.Reflection;

namespace Service.Billing.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Invoice> CAT_Factura { get; set; }
        public DbSet<InvoiceCompany> CAT_Factura_Companias { get; set; }
        public DbSet<InvoiceCompanyRequests> Relacion_Factura_Solicitudes { get; set; }
        public DbSet<Series> CAT_Serie { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
