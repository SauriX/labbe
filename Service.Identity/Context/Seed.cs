using DocumentFormat.OpenXml.Wordprocessing;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Identity.Domain.Menu;
using Service.Identity.Domain.Role;
using Service.Identity.Domain.User;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roles = Shared.Dictionary.Catalogs.Role;

namespace Service.Identity.Context
{
    public class Seed
    {
        public static async Task SeedData(ApplicationDbContext context, string key)
        {
            if (true)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var menus = GetMenus();
                    var menu = new Menu();

                    var script = MergeGenerator.Build(
                        nameof(context.CAT_Menu),
                        menus,
                        nameof(menu.Id),
                        nameof(menu.MenuPadreId),
                        nameof(menu.Descripcion),
                        nameof(menu.Controlador),
                        nameof(menu.Icono),
                        nameof(menu.Ruta),
                        nameof(menu.Orden),
                        nameof(menu.Activo));

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Menu)} ON;");
                    context.Database.ExecuteSqlRaw(script);
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Menu)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            if (!context.CAT_Rol.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var roles = GetRoles();
                    var role = new Role();

                    var script = MergeGenerator.Build(
                        nameof(context.CAT_Rol),
                        roles,
                        nameof(role.Id),
                        nameof(role.Nombre),
                        nameof(role.Activo));

                    context.Database.ExecuteSqlRaw(script);

                    var permissions = GetMenus().Select(x => new RolePermission(Roles.ADMIN, x.Id, true, true, true, true, true, true, true)).ToList();

                    await context.BulkInsertOrUpdateOrDeleteAsync(permissions);

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
                        Clave = "ADMIN",
                        Nombre = "ADMINISTRADOR",
                        PrimerApellido = "SISTEMA",
                        RolId = Roles.ADMIN,
                        SucursalId = Guid.Empty,
                        Contraseña = Crypto.EncryptString("12345678", key),
                        FlagPassword = true,
                        Activo = true,
                        FechaCreo = DateTime.Now,
                    };

                    context.CAT_Usuario.Add(user);
                    await context.SaveChangesAsync();

                    var permissions = GetMenus().Select(x => new UserPermission(userId, x.Id, true, true, true, true, true, true, true)).ToList();

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

        private static List<Menu> GetMenus()
        {
            var menus = new List<Menu>
            {
                // Configuracion de Catalogos -> Orden 1000
                new Menu(1, null, "Configuración", "cat", "", 1000),
                new Menu(2, 1, "Roles", "role", "roles", 1010),
                new Menu(3, 1, "Usuarios", "user", "users", 1020),
                new Menu(4, 1, "Sucursales", "branch", "branches", 1030),
                new Menu(5, 1, "Compañias", "company", "companies", 1040),
                new Menu(6, 1, "Médicos", "medic", "medics", 1050),
                new Menu(7, 1, "Estudios", "study", "studies", 1060),
                new Menu(8, 1, "Reactivos", "reagent", "reagents", 1070),
                new Menu(9, 1, "Indicaciones", "indication", "indications", 1080),
                new Menu(10, 1, "Parámetros", "parameter", "parameters", 1090),
                new Menu(11, 1, "General", "catalog", "catalogs", 1100),
                new Menu(12, 1, "Listas de precios", "price", "prices", 1110),
                new Menu(13, 1, "Paquetes", "pack", "packs", 1120),
                new Menu(14, 1, "Promociones en listas de precios", "promo", "promos", 1130),
                new Menu(15, 1, "Lealtades", "loyalty", "loyalties", 1140),
                new Menu(16, 1, "Etiquetas", "tag", "tags", 1150),
                new Menu(17, 1, "Rutas", "route", "routes", 1160),
                new Menu(18, 1, "Maquilador", "maquila", "maquila", 1170),
                new Menu(28, 1, "Admin. Equipos", "equipment", "equipment", 1180),
                new Menu(29, 1, "Mantenimiento", "mantain","equipmentMantain", 1190),

                // Parametros de sistema -> Orden 5000
                new Menu(20, null, "Parámetros de sistema", "configuration", "configuration", 5000),

                // Recepcion -> Orden 2000
                new Menu(24, null, "Recepción", "reception", "reception", 2000),
                new Menu(19, 24, "Expedientes", "expedientes", "expedientes", 2010),
                new Menu(21, 24, "Cotización", "quotation", "cotizacion", 2020),
                new Menu(23, 24, "Citas","appointments", "appointments", 2030),
                new Menu(27, 24, "Solicitudes", "request", "requests", 2040),
                new Menu(25, 24, "Toma de muestra", "sampling", "samplings", 2050),
                new Menu(26, 24, "Solicitar Estudio", "requestedstudy", "requestedstudy", 2060),
                new Menu(32, 24, "Envío de resultados", "deliveryResults","deliveryResults", 2070),
                new Menu(33, 24, "Seguimiento de rutas", "RouteTracking","segRutas", 2080),
                new Menu(34, 24, "Detalle de seguimiento de envio", "shipmentTracking", "shipmentTracking", 2090),

                // Resultados -> Orden 3000
                new Menu(37, null, "Resultados", "results", "results", 3000), // Ultimo
                new Menu(30, 37, "Captura de Resultados (Clínicos)", "clinicResults", "clinicResults", 3000),
                new Menu(36, 37, "Liberación de resultados", "relaseResult", "relaseResult", 3010),
                new Menu(35, 37, "Listas de trabajo", "worklist", "worklists", 3020),
                new Menu(31, 37, "Tablas de captura de resultados", "massSearch", "massResultSearch", 3030),

                // Reportes -> Orden 4000
                new Menu(22, null, "Reportes" , "report", "reports", 4000),

                // Facturacion -> Orden 6000
                new Menu(39, null, "Facturación", "invoice", "invoice", 6000),
                new Menu(40, 39, "Facturación por companía", "invoiceCompany", "invoice", 6010),
                 new Menu(41, 39, "Catalogo de facturas y recibos", "InvoiceCatalog", "invoiceCatalog", 6011),
            };

            return menus;
        }

        private static List<Role> GetRoles()
        {
            var roles = new List<Role>
            {
                new Role(Roles.ADMIN, "ADMINISTRADOR"),
                new Role(Roles.JEFELAB, "JEFE DE LABORATORIO"),
                new Role(Roles.JEFEREC, "JEFE DE RECEPCION (Coordinador de Operaciones)"),
                new Role(Roles.CONTA, "CONTABILIDAD"),
                new Role(Roles.FACT, "FACTURACION CONVENIOS"),
                new Role(Roles.PROC, "PROCESO (Proceso Analitico)"),
                new Role(Roles.ALM, "ALMACEN"),
                new Role(Roles.RECIMP, "RECEPCIÓN (Atencion y Caja)"),
            };

            return roles;
        }
    }
}