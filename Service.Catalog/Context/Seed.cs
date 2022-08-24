using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Domain.Branch;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEP = Shared.Dictionary.Catalogs.Department;

namespace Service.Catalog.Context
{
    public class Seed
    {
        public static async Task SeedData(ApplicationDbContext context, string key)
        {
            if (!context.CAT_Configuracion.Any())
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var configuration = new List<Configuration>()
                    {
                        new Configuration()
                        {
                            Id = 1,
                            Descripcion = "Correo",
                        },
                        new Configuration()
                        {
                            Id = 2,
                            Descripcion = "Remitente",
                        },
                        new Configuration()
                        {
                            Id = 3,
                            Descripcion = "SMTP",
                        },
                        new Configuration()
                        {
                            Id = 4,
                            Descripcion = "Requiere Contraseña",
                        },
                        new Configuration()
                        {
                            Id = 5,
                            Descripcion = "Contraseña",
                        },
                        new Configuration()
                        {
                            Id = 6,
                            Descripcion = "Nombre Sistema",
                        },
                        new Configuration()
                        {
                            Id = 7,
                            Descripcion = "Logo",
                            Valor = "logo.png"
                        },
                        new Configuration()
                        {
                            Id = 8,
                            Descripcion = "RFC",
                        },
                        new Configuration()
                        {
                            Id = 9,
                            Descripcion = "Razón Social",
                        },
                        new Configuration()
                        {
                            Id = 10,
                            Descripcion = "CP",
                        },
                        new Configuration()
                        {
                            Id = 11,
                            Descripcion = "Estado",
                        },
                        new Configuration()
                        {
                            Id = 12,
                            Descripcion = "Colonia",
                        },
                        new Configuration()
                        {
                            Id = 13,
                            Descripcion = "Calle",
                        },
                        new Configuration()
                        {
                            Id = 14,
                            Descripcion = "Número",
                        },
                        new Configuration()
                        {
                            Id = 15,
                            Descripcion = "Teléfono",
                        },
                        new Configuration()
                        {
                            Id = 16,
                            Descripcion = "Ciudad",
                        },
                    };

                    context.CAT_Configuracion.AddRange(configuration);

                    await context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            if (!context.CAT_Sucursal_Folio.Any())
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var ciudades = new List<BranchFolioConfig>()
                    {
                        // Sonora -> Ciudad Obregon
                        new BranchFolioConfig(26, 2059, 1, 1),    
                        // Sonora -> Navojoa
                        new BranchFolioConfig(26, 2042, 1, 2),  
                        // Sonora -> Hermosillo
                        new BranchFolioConfig(26, 2045, 1, 3),
                        // Sonora -> Nogales
                        new BranchFolioConfig(26, 2070, 1, 4),
                        // Sonora -> Guaymas
                        new BranchFolioConfig(26, 2105, 1, 5),
                        // Nuevo León -> Monterrey
                        new BranchFolioConfig(19, 1061, 2, 1),
                        // Nuevo León -> Nuevo León
                        new BranchFolioConfig(19, 1073, 2, 2),
                };

                    context.CAT_Sucursal_Folio.AddRange(ciudades);
                    await context.SaveChangesAsync();

                    transaction.Commit();

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            if (true)
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var departments = GetDepartments();

                    StringBuilder script = new($@"
                        MERGE {nameof(context.CAT_Departamento)} AS Target
                            USING (VALUES [VALUES]) AS Source(Id, Clave, Nombre, Activo)
                            ON Source.Id = Target.Id
                        WHEN NOT MATCHED BY Target THEN
                            INSERT (Id, Clave, Nombre, Activo)
                            VALUES (Source.Id, Source.Clave, Source.Nombre, Source.Activo)
                        WHEN MATCHED THEN UPDATE SET
                            Target.Clave = Source.Clave, 
	                        Target.Nombre = Source.Nombre,
	                        Target.Activo = Source.Activo;");

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Departamento)} ON;");

                    var values = string.Join("," + Environment.NewLine,
                        departments.Select(x => string.Concat("(", x.Id, ",'", x.Clave, "','", x.Nombre, "',", x.Activo ? 1 : 0, ")")));

                    script.Replace("[VALUES]", values);

                    context.Database.ExecuteSqlRaw(script.ToString());

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Departamento)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Departamento)} OFF;");
                    transaction.Rollback();
                    throw;
                }
            }

            if (true)
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var areas = GetAreas();

                    StringBuilder script = new($@"
                        MERGE {nameof(context.CAT_Area)} AS Target
                            USING (VALUES [VALUES]) AS Source(Id, DepartamentoId, Clave, Nombre, Activo)
                            ON Source.Id = Target.Id
                        WHEN NOT MATCHED BY Target THEN
                            INSERT (Id, DepartamentoId, Clave, Nombre, Activo)
                            VALUES (Source.Id, Source.DepartamentoId, Source.Clave, Source.Nombre, Source.Activo)
                        WHEN MATCHED THEN UPDATE SET
                            Target.DepartamentoId = Source.DepartamentoId, 
                            Target.Clave = Source.Clave, 
	                        Target.Nombre = Source.Nombre,
	                        Target.Activo = Source.Activo;");

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Area)} ON;");

                    var values = string.Join("," + Environment.NewLine,
                        areas.Select(x => string.Concat("(", x.Id, ",", x.DepartamentoId, ",'", x.Clave, "','", x.Nombre, "',", x.Activo ? 1 : 0, ")")));

                    script.Replace("[VALUES]", values);

                    context.Database.ExecuteSqlRaw(script.ToString());

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Area)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Area)} OFF;");
                    transaction.Rollback();
                    throw;
                }


                //using var transaction = context.Database.BeginTransaction();
                //try
                //{
                //    var area = new List<Area>(){
                //        new Area
                //    {
                //        Id = 1,
                //        Clave = "Paquetes",
                //        Nombre = "Paquetes",
                //        Activo = true,
                //        DepartamentoId = 1
                //    },
                //        new Area
                //    {
                //        Id = 2,
                //        Clave = "Imagenologia",
                //        Nombre = "Imagenologia",
                //        Activo = true,
                //        DepartamentoId = 2
                //    }
                //    };

                //    if (!context.CAT_Area.Any())
                //    {
                //        context.CAT_Area.AddRange(area);
                //    }
                //    else
                //    {
                //        context.CAT_Area.UpdateRange(area);
                //    }
                //    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.CAT_Area ON;");
                //    await context.SaveChangesAsync();
                //    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.CAT_Area OFF;");

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }


            if (!context.CAT_Units.Any())
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var unidades = new List<Units>
                    {
                       new Units{
                        Clave = "g/24 hr",
                        Nombre = "g/24 hr",
                        Activo = true,
                       },
                       new Units{
                        Clave = "0.2 µg/mL",
                        Nombre = "0.2 µg/mL",
                        Activo = true,
                       },
                       new Units{
                        Clave = "2.0 µg/mL",
                        Nombre = "2.0 µg/mL",
                        Activo = true,
                       },
                       new Units{
                            Clave = "mcg/g creatinina",
                            Nombre = "mcg/g creatinina",
                            Activo = true,
                       },
                       new Units{
                            Clave = "Log. cop./mL",
                            Nombre = "mLog. cop./mL",
                            Activo = true,
                       },
                       new Units{
                            Clave = "mg/24 h",
                            Nombre = "mg/24 h",
                            Activo = true,
                       },
                       new Units{

                            Clave = "U DILUCION",
                            Nombre = "U DILUCION",
                            Activo = true,
                       },
                       new Units{
                            Clave = "metros",
                            Nombre = "metros",
                            Activo = true,
                       },
                       new Units{
                            Clave = "g/12 HORAS",
                            Nombre = "g/12 HORAS",
                            Activo = true,
                       },
                       new Units{
                            Clave = "kU/I",
                            Nombre = "kU/I",
                            Activo = true,
                       },
                       new Units{
                            Clave = "µg/24 hrs",
                            Nombre = "µg/24 hrs",
                            Activo = true,
                       },
                       new Units{
                            Clave = "mg/g DE CREAT.",
                            Nombre = "mg/g DE CREAT.",
                            Activo = true,
                       },
                       new Units{
                            Clave = "nM/mM CREAT",
                            Nombre = "nM/mM CREAT",
                            Activo = true,
                       },
                       new Units{
                            Clave = "UE/mL",
                            Nombre = "UE/mL",
                            Activo = true,
                       },
                       new Units{
                            Clave = "mg/24 horas",
                            Nombre = "mg/24 horas",
                            Activo = true,
                       },
                       new Units{
                            Clave = "nmolBCE/mmolcr",
                            Nombre = "nmolBCE/mmolcr",
                            Activo = true,
                        },
                        new Units{
                            Clave = "U.BETHESDA",
                            Nombre = "U.BETHESDA",
                            Activo = true,
                        },
                        new Units{
                            Clave = "cp/mL",
                            Nombre = "cp/mL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mg/2hrs",
                            Nombre = "mg/2hrs",
                            Activo = true,
                        },
                        new Units{
                            Clave = "µg/24h",
                            Nombre = "µg/24h",
                            Activo = true,
                        },
                        new Units{
                            Clave = "U.BETHESDA/mL",
                            Nombre = "U.BETHESDA/mL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "m²",
                            Nombre = "m²",
                            Activo = true,
                        },
                        new Units{
                            Clave = "µmol/EYACULADO",
                            Nombre = "µmol/EYACULADO",
                            Activo = true,
                        },
                        new Units{
                            Clave = "µM/L",
                            Nombre = "µM/L",
                            Activo = true,
                        },
                        new Units{
                            Clave = "µg/dl DE ERIT.",
                            Nombre = "µg/dl DE ERIT.",
                            Activo = true,
                        },
                        new Units{
                            Clave = "nmol",
                            Nombre = "nmol",
                            Activo = true,
                        },
                        new Units{
                            Clave = "MoM",
                            Nombre = "MoM",
                            Activo = true,
                        },
                        new Units{
                            Clave = "COCIENTE NORMALIZADO",
                            Nombre = "COCIENTE NORMALIZADO",
                            Activo = true,
                        },
                        new Units{
                            Clave = "g/72hrs",
                            Nombre = "g/72hrs",
                            Activo = true,
                        },
                        new Units{
                            Clave = "ug/min",
                            Nombre = "ug/min",
                            Activo = true,
                        },
                        new Units{
                            Clave = "U/10Exp12 ERITRO",
                            Nombre = "U/10Exp12 ERITRO",
                            Activo = true,
                        },
                        new Units{
                            Clave = "nM DPD/mM",
                            Nombre = "nM DPD/mM",
                            Activo = true,
                        },
                        new Units{
                            Clave = "ug/ mL",
                            Nombre = "ug/ mL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "U/CH 50",
                            Nombre = "U/CH 50",
                            Activo = true,
                        },
                        new Units{
                            Clave = "M.L.P.",
                            Nombre = "M.L.P.",
                            Activo = true,
                        },
                        new Units{
                            Clave = "G.L.P.",
                            Nombre = "G.L.P.",
                            Activo = true,
                        },
                        new Units{
                            Clave = "ug / 24 hr",
                            Nombre = "ug / 24 hr",
                            Activo = true,
                        },
                        new Units{
                            Clave = "COI",
                            Nombre = "COI",
                            Activo = true,
                        },
                        new Units{
                            Clave = "Ul/L",
                            Nombre = "Ul/L",
                            Activo = true,
                        },
                        new Units{
                            Clave = "U/dL",
                            Nombre = "U/dL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mmo/L",
                            Nombre = "mmo/L",
                            Activo = true,
                        },
                        new Units{
                            Clave = "UI / L",
                            Nombre = "UI / L",
                            Activo = true,
                        },
                        new Units{
                            Clave = "IU/ ml",
                            Nombre = "IU/ ml",
                            Activo = true,
                        },
                        new Units{
                            Clave = "uM/L",
                            Nombre = "uM/L",
                            Activo = true,
                        },
                        new Units{
                            Clave = "% Colesterol total",
                            Nombre = "% Colesterol total",
                            Activo = true,
                        },
                        new Units{
                            Clave = "ml/24 Hrs.",
                            Nombre = "ml/24 Hrs.",
                            Activo = true,
                        },
                        new Units{
                            Clave = "E.U./dl",
                            Nombre = "E.U./dl",
                            Activo = true,
                        },
                        new Units{
                            Clave = "/mm³",
                            Nombre = "/mm³",
                            Activo = true,
                        },
                        new Units{
                            Clave = "/mm3",
                            Nombre = "/mm3",
                            Activo = true,
                        },
                        new Units{
                            Clave = "U/gHI",
                            Nombre = "U/gHI",
                            Activo = true,
                        },
                        new Units{
                            Clave = "/ CAMPO",
                            Nombre = "/ CAMPO",
                            Activo = true,
                        },
                        new Units{
                        Clave = "Celulas /uL",
                        Nombre = "Celulas /uL",
                        Activo = true,
                        },
                        new Units{
                            Clave = "10^3/mm3",
                            Nombre = "10^3/mm3",
                            Activo = true,
                        },
                        new Units{
                            Clave = "cels /uL",
                            Nombre = "cels /uL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "OSB",
                            Nombre = "OSB",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mcg/24hs",
                            Nombre = "mcg/24hs",
                            Activo = true,
                        },
                        new Units{
                            Clave = "g/24 Hrs.",
                            Nombre = "g/24 Hrs.",
                            Activo = true,
                        },

                        new Units{
                            Clave = "m2",
                            Nombre = "m2",
                            Activo = true,
                        },
                        new Units{
                            Clave = "Por ml",
                            Nombre = "Por ml",
                            Activo = true,
                        },
                        new Units{
                            Clave = "UROPORFIRINAS/mL DE ERITROCITOS/2hrs",
                            Nombre = "UROPORFIRINAS/mL DE ERITROCITOS/2hrs",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mlu/mL",
                            Nombre = "mlu/mL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "Log IU/mL",
                            Nombre = "Log IU/mL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "MICRAS/SEG",
                            Nombre = "MICRAS/SEG",
                            Activo = true,
                        },
                        new Units{
                            Clave = "g.",
                            Nombre = "g.",
                            Activo = true,
                        },
                        new Units{
                            Clave = "ng / mL",
                            Nombre = "ng / mL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "Unidades de tuberculina",
                            Nombre = "Unidades de tuberculina",
                            Activo = true,
                        },
                        new Units{
                            Clave = "g/100 mL",
                            Nombre = "g/100 mL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "cels/ uL",
                            Nombre = "cels/ uL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "U/ L",
                            Nombre = "U/ L",
                            Activo = true,
                        },
                        new Units{
                            Clave = "LEUCOCITOS/mL",
                            Nombre = "LEUCOCITOS/mL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "umoL/EYACULADO",
                            Nombre = "umoL/EYACULADO",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mOsmol/Kg",
                            Nombre = "mOsmol/Kg",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mmol/24h",
                            Nombre = "mmol/24h",
                            Activo = true,
                        },
                        new Units{
                            Clave = "nMDPD/mMcr",
                            Nombre = "nMDPD/mMcr",
                            Activo = true,
                        },
                        new Units{
                            Clave = "nmol DPD/mmol CREAT",
                            Nombre = "nmol DPD/mmol CREAT",
                            Activo = true,
                        },
                        new Units{
                            Clave = "3",
                            Nombre = "3",
                            Activo = true,
                        },
                        new Units{
                            Clave = "UFC/g",
                            Nombre = "UFC/g",
                            Activo = true,
                        },
                        new Units{
                            Clave = "UFC/ g",
                            Nombre = "UFC/ g",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mcg/ 24 HRS",
                            Nombre = "mcg/ 24 HRS",
                            Activo = true,
                        },
                        new Units{
                            Clave = "% DEL TOTAL",
                            Nombre = "% DEL TOTAL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "g / 5 horas",
                            Nombre = "g / 5 horas",
                            Activo = true,
                        },
                        new Units{
                            Clave = "U JDF",
                            Nombre = "U JDF",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mg/24 hr",
                            Nombre = "mg/24 hr",
                            Activo = true,
                        },
                        new Units{
                            Clave = "ULog",
                            Nombre = "ULog",
                            Activo = true,
                        },
                        new Units{
                            Clave = "g/12hr",
                            Nombre = "g/12hr",
                            Activo = true,
                        },
                        new Units{
                            Clave = "g/24hr",
                            Nombre = "g/24hr",
                            Activo = true,
                        },
                        new Units{
                            Clave = "pg",
                            Nombre = "pg",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mg/2hr",
                            Nombre = "mg/2hr",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mg/12HRS",
                            Nombre = "mg/12HRS",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mg/mL",
                            Nombre = "mg/mL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "LEU/CAMPO",
                            Nombre = "LEU/CAMPO",
                            Activo = true,
                        },
                        new Units{
                            Clave = "UI/24h",
                            Nombre = "UI/24h",
                            Activo = true,
                        },
                        new Units{
                            Clave = "µ/dl",
                            Nombre = "µ/dl",
                            Activo = true,
                        },
                        new Units{
                            Clave = "U MPL",
                            Nombre = "U MPL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "U GPL",
                            Nombre = "U GPL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "ERI/CAMPO",
                            Nombre = "ERI/CAMPO",
                            Activo = true,
                        },
                        new Units{
                            Clave = "ugs %",
                            Nombre = "ugs %",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mU/L",
                            Nombre = "mU/L",
                            Activo = true,
                        },
                        new Units{
                            Clave = "ml/24hr",
                            Nombre = "ml/24hr",
                            Activo = true,
                        },
                        new Units{
                            Clave = "IU/I",
                            Nombre = "IU/I",
                            Activo = true,
                        },
                        new Units{
                            Clave = "LogIU/mL",
                            Nombre = "LogIU/mL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "UFC/ml",
                            Nombre = "UFC/ml",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mEu/L",
                            Nombre = "mEu/L",
                            Activo = true,
                        },
                        new Units{
                            Clave = "C.M.I.  ug/mL",
                            Nombre = "C.M.I.  ug/mL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "A.L.P.",
                            Nombre = "A.L.P.",
                            Activo = true,
                        },
                        new Units{
                            Clave = "uEq/min",
                            Nombre = "uEq/min",
                            Activo = true,
                        },
                        new Units{
                            Clave = "uE/min",
                            Nombre = "uE/min",
                            Activo = true,
                        },
                        new Units{
                            Clave = "g/5 horas",
                            Nombre = "g/5 horas",
                            Activo = true,
                        },
                        new Units{
                            Clave = "UR/mL",
                            Nombre = "UR/mL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "Min.",
                            Nombre = "Min.",
                            Activo = true,
                        },
                        new Units{
                            Clave = "S/CO",
                            Nombre = "S/CO",
                            Activo = true,
                        },
                        new Units{
                            Clave = "LEU/ml",
                            Nombre = "LEU/ml",
                            Activo = true,
                        },
                        new Units{
                            Clave = "UI/dL",
                            Nombre = "UI/dL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "DPM",
                            Nombre = "DPM",
                            Activo = true,
                        },
                        new Units{
                            Clave = "ug/mg de creatinina",
                            Nombre = "ug/mg de creatinina",
                            Activo = true,
                        },
                        new Units{
                            Clave = "ug/g",
                            Nombre = "ug/g",
                            Activo = true,
                        },
                        new Units{
                            Clave = "U APL",
                            Nombre = "U APL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mL/24 h",
                            Nombre = "mL/24 h",
                            Activo = true,
                        },
                        new Units{
                            Clave = "respiraciones/minuto",
                            Nombre = "respiraciones/minuto",
                            Activo = true,
                        },
                        new Units{
                            Clave = "latidos/minuto",
                            Nombre = "latidos/minuto",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mg/mmol",
                            Nombre = "mg/mmol",
                            Activo = true,
                        },
                        new Units{
                            Clave = "RU/mL",
                            Nombre = "RU/mL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mL/min/m2",
                            Nombre = "mL/min/m2",
                            Activo = true,
                        },
                        new Units{
                            Clave = "ug/24Hrs.",
                            Nombre = "ug/24Hrs.",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mmol / L",
                            Nombre = "mmol / L",
                            Activo = true,
                        },
                        new Units{
                            Clave = "uUl/mL",
                            Nombre = "uUl/mL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mg/g",
                            Nombre = "mg/g",
                            Activo = true,
                        },
                        new Units{
                            Clave = "µg/mL",
                            Nombre = "µg/mL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "% DE ACTIVIDAD",
                            Nombre = "% DE ACTIVIDAD",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mcg/g",
                            Nombre = "mcg/g",
                            Activo = true,
                        },
                        new Units{
                            Clave = "A.P.L",
                            Nombre = "A.P.L",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mg/g creatinina",
                            Nombre = "mg/g creatinina",
                            Activo = true,
                        },
                        new Units{
                            Clave = "ng/24 HORAS",
                            Nombre = "ng/24 HORAS",
                            Activo = true,
                        },
                        new Units{
                            Clave = "mg/g de creat",
                            Nombre = "mg/g de creat",
                            Activo = true,
                        },
                        new Units{
                            Clave = "BETHESDA/mL",
                            Nombre = "BETHESDA/mL",
                            Activo = true,
                        },
                        new Units{
                            Clave = "nM BCE/mM Cr",
                            Nombre = "nM BCE/mM Cr",
                            Activo = true,
                        },

                    };

                    context.CAT_Units.AddRange(unidades);


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

        private static List<Department> GetDepartments()
        {
            var departments = new List<Department>
            {
                new Department(DEP.PAQUETES, "PAQ", "PAQUETES"),
                new Department(DEP.IMAGENOLOGIA, "IMAGENOLOGÍA", "IMAGENOLOGÍA"),
                new Department(DEP.PATOLOGIA, "PATOLOGÍA", "PATOLOGÍA"),
                new Department(DEP.LABORATORIO, "LABORATORIO", "LABORATORIO"),
            };

            return departments;
        }

        private static List<Area> GetAreas()
        {
            var areas = new List<Area>
            {
                new Area(1, DEP.PAQUETES, "PAQ", "PAQUETES"),
                new Area(2, DEP.LABORATORIO, "ANA", "AUTOINMUNIDAD"),
                new Area(3, DEP.LABORATORIO, "APE", "ANTIGENO PROSTATICO"),
                new Area(4, DEP.PATOLOGIA, "BACTER", "BACTERIOLOGIA"),
                new Area(5, DEP.LABORATORIO, "BH", "BIOMETRIAS"),
                new Area(6, DEP.LABORATORIO, "CITO", "CITOMETRIA DE FLUJO"),
                new Area(7, DEP.LABORATORIO, "CLAMI", "CLAMIDIA"),
                new Area(8, DEP.PATOLOGIA, "CN", "CITOLOGIA NASAL"),
                new Area(9, DEP.LABORATORIO, "COAG", "COAGULACION"),
                new Area(10, DEP.LABORATORIO, "CONVU", "ANTICONVULSIVANTES"),
                new Area(11, DEP.LABORATORIO, "COPROL", "COPROLOGICOS"),
                new Area(12, DEP.LABORATORIO, "DOP", "ANTIDOPING"),
                new Area(13, DEP.LABORATORIO, "ELECS", "ELECTROLITOS SERICOS"),
                new Area(14, DEP.LABORATORIO, "ELECU", "ELECTROLITOS URINARIOS"),
                new Area(15, DEP.LABORATORIO, "ESP", "ESPERMATOBIOSCOPIA"),
                new Area(16, DEP.LABORATORIO, "ESPE", "PRUEBAS ESPECIALES"),
                new Area(17, DEP.LABORATORIO, "FEB", "REACCIONES FEBRILES"),
                new Area(18, DEP.LABORATORIO, "GAS", "PRUEBAS GAS"),
                new Area(19, DEP.LABORATORIO, "GIN", "GINECOLOGICO"),
                new Area(20, DEP.LABORATORIO, "GRU", "GRUPO SANGUINEO"),
                new Area(21, DEP.LABORATORIO, "HEMATO", "HEMATOLOGIA"),
                new Area(22, DEP.LABORATORIO, "HEPA", "HEPATITIS"),
                new Area(23, DEP.LABORATORIO, "HOR", "HORMONAS"),
                new Area(24, DEP.LABORATORIO, "IDR", "INTRADERMORREACCIONES"),
                new Area(25, DEP.LABORATORIO, "IG", "INMUNOGLOBULINAS"),
                new Area(26, DEP.LABORATORIO, "LENDO1", "LENDO1"),
                new Area(27, DEP.LABORATORIO, "LIQUI", "LIQUIDOS"),
                new Area(28, DEP.LABORATORIO, "ONCO", "MARCADORES TUMORALES"),
                new Area(29, DEP.LABORATORIO, "PARA", "PARASITOLOGIA"),
                new Area(30, DEP.PATOLOGIA, "HIPATO", "HISTOPATOLOGÍA"),
                new Area(31, DEP.LABORATORIO, "PCR", "CARGAS VIRALES"),
                new Area(32, DEP.LABORATORIO, "PRE", "PRENUPCIALES"),
                new Area(33, DEP.LABORATORIO, "PRU", "AREA PRUEBA"),
                new Area(34, DEP.LABORATORIO, "QS", "QUIMICAS"),
                new Area(35, DEP.LABORATORIO, "RIA", "EMBARAZO"),
                new Area(36, DEP.IMAGENOLOGIA, "RX", "RAYOS X"),
                new Area(37, DEP.LABORATORIO, "SA", "SIN AREA"),
                new Area(38, DEP.LABORATORIO, "SERO", "SEROLOGIA"),
                new Area(39, DEP.LABORATORIO, "TIR", "TIROIDEOS"),
                new Area(40, DEP.LABORATORIO, "TORCH", "TORCH"),
                new Area(41, DEP.LABORATORIO, "URI", "URIANALISIS"),
                new Area(42, DEP.LABORATORIO, "VDRL", "VDRL"),
                new Area(43, DEP.LABORATORIO, "XXX", "INFORMATIVA"),
                new Area(44, DEP.LABORATORIO, "INMUNO", "INMUNO"),
                new Area(45, DEP.LABORATORIO, "GLICO", " GLICOSILADAS"),
                new Area(46, DEP.LABORATORIO, "ELECTR", "ELECTROFORESIS"),
                new Area(47, DEP.LABORATORIO, "BM", "BIOLOGIA MOLECULAR"),
                new Area(48, DEP.LABORATORIO, "PCRGEN", "PCR GENNE XPERT"),
                new Area(49, DEP.IMAGENOLOGIA, "ULTSO", "ULTRASONIDO"),
                new Area(50, DEP.IMAGENOLOGIA, "MASTO", "MASTOGRAFÍA"),
            };

            return areas;
        }
    }
}
