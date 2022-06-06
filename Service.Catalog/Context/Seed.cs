using Microsoft.EntityFrameworkCore;
using Service.Catalog.Domain.Catalog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Context
{
    public class Seed
    {
        public static async Task SeedData(ApplicationDbContext context, string key) {

            if (!context.CAT_Departamento.Any() || context.CAT_Departamento.Any(x => x.Id == 1 && x.Nombre != "Paquetes"))
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var departamento = new Department
                    {
                        Id = 1,
                        Clave = "Paquetes",
                        Nombre = "paquetes",
                        Activo = true,
                    };
                    if (!context.CAT_Departamento.Any())
                    {
                        context.CAT_Departamento.AddRange(departamento);
                    }
                    else {
                        context.CAT_Departamento.UpdateRange(departamento);
                    }
                    

                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.CAT_Departamento ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.CAT_Departamento OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            if (!context.CAT_Area.Any() || context.CAT_Area.Any(x=> x.Id==1 && x.Nombre!="Paquetes"))
            {

                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var area = new Area
                    {
                        Id = 1,
                        Clave = "Paquetes",
                        Nombre = "Paquetes",
                        Activo = true,
                        DepartamentoId = 1
                    };

                    if (!context.CAT_Departamento.Any())
                    {
                        context.CAT_Area.AddRange(area);
                    }
                    else {
                        context.CAT_Area.UpdateRange(area);
                    }
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.CAT_Area ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.CAT_Area OFF;");

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
