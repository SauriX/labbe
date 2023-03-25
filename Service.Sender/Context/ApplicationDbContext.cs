using Microsoft.EntityFrameworkCore;
using Service.Sender.Domain.EmailConfiguration;
using Service.Sender.Domain.NotificationHistory;
using System.Reflection;

namespace Service.Sender.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<EmailConfiguration> CAT_Email { get; set; }
        public DbSet<NotificationHistory> CAT_Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
