using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Service.Identity.Domain.Menu;
using Service.Identity.Domain.permissions;
using Service.Identity.Domain.Role;
using Service.Identity.Domain.User;
using System;
using System.Reflection;

namespace Service.Identity.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Menu> CAT_Menu { get; set; }
        //public DbSet<Permission> CAT_Permisos { get; set; }
        public DbSet<User> CAT_Usuario { get; set; }
        public DbSet<UserPermission> CAT_Usuario_Permiso { get; set; }
        public DbSet<Role> CAT_Rol { get; set; }
        public DbSet<RolePermission> CAT_Rol_Permiso { get; set; }
        //public DbSet<RolPermiso> Relacion_Rol_PermisoEspecial { get; set; }
        //public DbSet<SpecialPermissions> CAT_PermisoEspecial { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

