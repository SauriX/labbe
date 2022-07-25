using Microsoft.EntityFrameworkCore;
using Service.Identity.Domain.Menu;
using Service.Identity.Domain.Role;
using Service.Identity.Domain.User;
using System.Reflection;

namespace Service.Identity.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Menu> CAT_Menu { get; set; }
        public DbSet<User> CAT_Usuario { get; set; }
        public DbSet<UserPermission> CAT_Usuario_Permiso { get; set; }
        public DbSet<Role> CAT_Rol { get; set; }
        public DbSet<RolePermission> CAT_Rol_Permiso { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

