using Microsoft.EntityFrameworkCore;
using Service.Billing.Domain.Invoice;
using System.Reflection;

namespace Service.Billing.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Invoice> CAT_Factura { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
