using Microsoft.EntityFrameworkCore;
using Service.Sender.Domain.EmailConfiguration;
using System.Reflection;

namespace Service.Sender.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<EmailConfiguration> CAT_Email { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
