using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Service.Identity.Domain.permissions;
using Service.Identity.Domain.Users;
using Service.Identity.Domain.UsersRol;
using System;

namespace Service.Identity.Context
{
    public class IndentityContext : IdentityDbContext<UsersModel,UserRol,Guid>
    {
        public IndentityContext(DbContextOptions<IndentityContext> options) : base(options)
        {
           
        }
        public DbSet<Permission> CAT_Permisos { get; set; }
        public DbSet<RolPermiso> Relacion_Rol_PermisoEspecial { get; set; }
        public DbSet<SpecialPermissions> CAT_PermisoEspecial { get; set; }
    }
}

