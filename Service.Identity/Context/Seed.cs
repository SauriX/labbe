using Microsoft.EntityFrameworkCore;
using Service.Identity.Domain.Menu;
using Service.Identity.Domain.Role;
using Service.Identity.Domain.User;
using Service.Identity.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Identity.Context
{
    public class Seed
    {
        public static async Task SeedData(ApplicationDbContext context, string key)
        {
            if (!context.CAT_Menu.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var menus = new List<Menu>
                    {
                        new Menu(id: 1, menuPadreId: null, descripcion: "Configuración", controlador: null, ruta: null, orden: 100),
                        new Menu(2, 1, "Roles", "role", "roles", 1001),
                        new Menu(3, 1, "Usuarios", "user", "users", 1002),
                        new Menu(4, 1, "Sucursales", "branch", "branches", 1003),
                        new Menu(5, 1, "Compañias", "company", "companies", 1004),
                        new Menu(6, 1, "Médicos", "medic", "medics", 1005),
                        new Menu(7, 1, "Estudios", "study", "studies", 1006),
                        new Menu(8, 1, "Reactivos", "reagent", "reagents", 1007),
                        new Menu(9, 1, "Indicaciones", "indication", "indications", 1008),
                        new Menu(10, 1, "Parámetros", "parameter", "parameters", 1009),
                        new Menu(11, 1, "General", "catalog", "catalogs", 1010),
                        new Menu(12, 1, "Listas de precios", "price", "prices", 1011),
                        new Menu(13, 1, "Paquetes", "pack", "packs", 1012),
                        new Menu(14, 1, "Promociones en listas de precios", "promo", "promos", 1013),
                        new Menu(15, 1, "Lealtades", "loyalty", "loyalties", 1014),
                        new Menu(16, 1, "Etiquetas", "tag", "tags", 1015),
                        new Menu(17, 1, "Rutas", "route", "routes", 1016),
                    };

                    context.CAT_Menu.AddRange(menus);

                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.CAT_Menu ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.CAT_Menu OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            if (context.CAT_Menu.Count() == 17)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var menus = new List<Menu>
                    {
                        new Menu(18, 1, "Maquilador", "maquila", "maquila", 1017),
                        new Menu(19, 1, "Expedientes", "expedientes", "expedientes", 1018),
                    };

                    context.CAT_Menu.AddRange(menus);

                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.CAT_Menu ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.CAT_Menu OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            var roleId = Guid.NewGuid();

            if (!context.CAT_Rol.Any())
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var role = new Role
                    {
                        Id = roleId,
                        Nombre = "Administrador",
                        Activo = true,
                        FechaCreo = DateTime.Now,
                    };

                    context.CAT_Rol.Add(role);
                    //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.CAT_Rol ON;");
                    await context.SaveChangesAsync();
                    //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.CAT_Rol OFF;");

                    var permissions = new List<RolePermission>
                    {
                        new RolePermission(roleId, 1, true, true, true, true, true, true, true),
                        new RolePermission(roleId, 2, true, true, true, true, true, true, true),
                        new RolePermission(roleId, 3, true, true, true, true, true, true, true),
                        new RolePermission(roleId, 4, true, true, true, true, true, true, true),
                        new RolePermission(roleId, 5, true, true, true, true, true, true, true),
                        new RolePermission(roleId, 6, true, true, true, true, true, true, true),
                        new RolePermission(roleId, 7, true, true, true, true, true, true, true),
                        new RolePermission(roleId, 8, true, true, true, true, true, true, true),
                        new RolePermission(roleId, 9, true, true, true, true, true, true, true),
                        new RolePermission(roleId, 10, true, true, true, true, true, true, true),
                        new RolePermission(roleId, 11, true, true, true, true, true, true, true),
                        new RolePermission(roleId, 12, true, true, true, true, true, true, true),
                        new RolePermission(roleId, 13, true, true, true, true, true, true, true),
                        new RolePermission(roleId, 14, true, true, true, true, true, true, true),
                        new RolePermission(roleId, 15, true, true, true, true, true, true, true),
                        new RolePermission(roleId, 16, true, true, true, true, true, true, true),
                        new RolePermission(roleId, 17, true, true, true, true, true, true, true),
                    };

                    context.CAT_Rol_Permiso.AddRange(permissions);
                    await context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            if (!context.CAT_Usuario.Any())
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var userId = Guid.NewGuid();

                    var user = new User
                    {
                        Id = userId,
                        Clave = "Admin",
                        Nombre = "Administrador",
                        PrimerApellido = "Sistema",
                        RolId = roleId,
                        Contraseña = Crypto.EncryptString("12345678", key),
                        FlagPassword = true,
                        Activo = true,
                        FechaCreo = DateTime.Now,
                     
                    };

                    context.CAT_Usuario.Add(user);
                    //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.CAT_Usuario ON;");
                    await context.SaveChangesAsync();
                    //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.CAT_Usuario OFF;");

                    var permissions = new List<UserPermission>
                    {
                        new UserPermission(userId, 1, true, true, true, true, true, true, true),
                        new UserPermission(userId, 2, true, true, true, true, true, true, true),
                        new UserPermission(userId, 3, true, true, true, true, true, true, true),
                        new UserPermission(userId, 4, true, true, true, true, true, true, true),
                        new UserPermission(userId, 5, true, true, true, true, true, true, true),
                        new UserPermission(userId, 6, true, true, true, true, true, true, true),
                        new UserPermission(userId, 7, true, true, true, true, true, true, true),
                        new UserPermission(userId, 8, true, true, true, true, true, true, true),
                        new UserPermission(userId, 9, true, true, true, true, true, true, true),
                        new UserPermission(userId, 10, true, true, true, true, true, true, true),
                        new UserPermission(userId, 11, true, true, true, true, true, true, true),
                        new UserPermission(userId, 12, true, true, true, true, true, true, true),
                        new UserPermission(userId, 13, true, true, true, true, true, true, true),
                        new UserPermission(userId, 14, true, true, true, true, true, true, true),
                        new UserPermission(userId, 15, true, true, true, true, true, true, true),
                        new UserPermission(userId, 16, true, true, true, true, true, true, true),
                        new UserPermission(userId, 17, true, true, true, true, true, true, true),
                    };

                    context.CAT_Usuario_Permiso.AddRange(permissions);
                    await context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
