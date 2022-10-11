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
using BR = Shared.Dictionary.Catalogs.Branch;
using Shared.Utils;
using ClosedXML.Excel;
using System.Data;
using Service.Catalog.Domain.Maquila;
using Service.Catalog.Domain.Indication;
using Service.Catalog.Domain.Tapon;
using Service.Catalog.Domain.Study;
using RabbitMQ.Client;
using Service.Catalog.Domain.Parameter;

namespace Service.Catalog.Context
{
    public class Seed
    {
        public static async Task SeedData(ApplicationDbContext context, string key)
        {
            if (true)
            {
                var methods = GetMethods();
                var payment = GetPaymentForms();
                var cfdi = GetUseOfCFDI​s();
                var deps = GetDepartments();
                var areas = GetAreas();
                var maquilas = GetMaquilas();
                var indications = GetIndications();
                var tags = GetTags();
                var studies = GetStudies();
                var branches = GetBranches();
            }

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
                        // Sonora -> Heroica Nogales
                        new BranchFolioConfig(26, 2066, 1, 4),
                        // Sonora -> Heroica Guaymas
                        new BranchFolioConfig(26, 2041, 1, 5),
                        // Nuevo León -> Monterrey
                        new BranchFolioConfig(19, 1061, 2, 1),
                        // Nuevo León -> San Pedro
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
                GetMethods();
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
                        departments.Select(x => $"({x.Id}, '{x.Clave}', '{x.Nombre}', {(x.Activo ? 1 : 0)})"));

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
                        areas.Select(x => $"({x.Id}, {x.DepartamentoId}, '{x.Clave}', '{x.Nombre}', {(x.Activo ? 1 : 0)})"));

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
            }

            if (true)
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var branches = GetBranches();

                    StringBuilder script = new($@"
                        MERGE {nameof(context.CAT_Sucursal)} AS Target
                            USING (VALUES [VALUES]) AS Source(Id, Clave, Nombre, Clinicos, Telefono, Correo, Calle, NumeroExterior, NumeroInterior, Codigopostal, ColoniaId, Ciudad, Estado, Matriz, Activo)
                            ON Source.Id = Target.Id
                        WHEN NOT MATCHED BY Target THEN
                            INSERT (Id, Clave, Nombre, Clinicos, Telefono, Correo, Calle, NumeroExterior, NumeroInterior, Codigopostal, ColoniaId, Ciudad, Estado, Matriz, Activo)
                            VALUES (Source.Id, Source.Clave, Source.Nombre, Source.Clinicos, Source.Telefono, Source.Correo, Source.Calle, Source.NumeroExterior, Source.NumeroInterior, Source.Codigopostal, Source.ColoniaId, Source.Ciudad, Source.Estado, Source.Matriz, Source.Activo)
                        WHEN MATCHED THEN UPDATE SET
                            Target.Nombre = Source.Nombre, 
	                        Target.Clinicos = Source.Clinicos,
	                        Target.Telefono = Source.Telefono,
	                        Target.Correo = Source.Correo,
	                        Target.Calle = Source.Calle,
	                        Target.NumeroExterior = Source.NumeroExterior,
	                        Target.NumeroInterior = Source.NumeroInterior,
	                        Target.Codigopostal = Source.Codigopostal,
	                        Target.ColoniaId = Source.ColoniaId,
	                        Target.Ciudad = Source.Ciudad,
	                        Target.Estado = Source.Estado,
	                        Target.Matriz = Source.Matriz,
	                        Target.Activo = Source.Activo;");

                    var values = string.Join("," + Environment.NewLine,
                        branches.Select(x => $"('{x.Id}', '{x.Clave}', '{x.Nombre}', '{x.Clinicos}', '{x.Telefono}', '{x.Correo}', '{x.Calle}', '{x.NumeroExterior}', '{x.NumeroInterior}', '{x.Codigopostal}', {x.ColoniaId}, '{x.Ciudad}', '{x.Estado}', {(x.Matriz ? 1 : 0)}, {(x.Activo ? 1 : 0)})"));

                    script.Replace("[VALUES]", values);

                    context.Database.ExecuteSqlRaw(script.ToString());

                    transaction.Commit();
                }
                catch (Exception)
                {
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Area)} OFF;");
                    transaction.Rollback();
                    throw;
                }
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

        private static List<Method> GetMethods()
        {
            var path = "wwwroot/seed/CAT_METODOS.xlsx";
            var tableData = ReadAsTable(path);

            var methods = tableData.AsEnumerable().Select(x => new Method(
                Convert.ToInt32(x.Field<double>("Id")),
                x.Field<string>("Clave"),
                x.Field<string>("Nombre") ?? ""
                )).ToList();

            return methods;
        }

        private static List<Payment> GetPaymentForms()
        {
            var paymentForms = new List<Payment>
            {
                new Payment(1, "01", "EF", "Efectivo"),
                new Payment(2, "02", "CN", "Cheque nominativo"),
                new Payment(3, "03", "TE", "Transferencia electrónica de fondos"),
                new Payment(4, "04", "TC", "Tarjeta de crédito"),
                new Payment(5, "05", "PP", "Monedero electrónico"),
                new Payment(6, "06", "DE", "Dinero electrónico"),
                new Payment(7, "08", "VD", "Vales de despensa"),
                new Payment(8, "12", "DP", "Dación en pago"),
                new Payment(9, "13", "PS", "Pago por subrogación"),
                new Payment(10, "14", "PC", "Pago por consignación"),
                new Payment(11, "15", "CD", "Condonación"),
                new Payment(12, "17", "CP", "Compensación"),
                new Payment(13, "23", "NV", "Novación"),
                new Payment(14, "24", "CF", "Confusión"),
                new Payment(15, "25", "RD", "Remisión de deuda"),
                new Payment(16, "26", "PR", "Prescripción o caducidad"),
                new Payment(17, "27", "SA", "A satisfacción del acreedor"),
                new Payment(18, "28", "TD", "Tarjeta de débito"),
                new Payment(19, "29", "TS", "Tarjeta de servicios"),
                new Payment(20, "30", "AA", "Aplicación de anticipos"),
                new Payment(21, "31", "IP", "Intermediario pagos"),
                new Payment(21, "99", "PD", "Por definir"),
            };

            return paymentForms;
        }

        private static List<UseOfCFDI​> GetUseOfCFDI​s()
        {
            var cfdis = new List<UseOfCFDI>
            {
                new UseOfCFDI(1, "G01", "G01", "Adquisición de mercancías"),
                new UseOfCFDI(2, "G02", "G02", "Devoluciones, descuentos o bonificaciones"),
                new UseOfCFDI(3, "G03", "G03", "Gastos en general"),
                new UseOfCFDI(4, "I01", "I01", "Construcciones"),
                new UseOfCFDI(5, "I02", "I02", "Mobiliario y equipo de oficina por inversiones"),
                new UseOfCFDI(6, "I03", "I03", "Equipo de transporte"),
                new UseOfCFDI(7, "I04", "I04", "Equipo de cómputo y accesorios"),
                new UseOfCFDI(8, "I05", "I05", "Dados, troqueles, moldes, matrices y herramental"),
                new UseOfCFDI(9, "I06", "I06", "Comunicaciones telefónicas"),
                new UseOfCFDI(10, "I07", "I07", "Comunicaciones satelitales"),
                new UseOfCFDI(11, "I08", "I08", "Otra maquinaria y equipo"),
                new UseOfCFDI(12, "D01", "D01", "Honorarios médicos, dentales y gastos hospitalarios."),
                new UseOfCFDI(13, "D02", "D02", "Gastos médicos por incapacidad o discapacidad"),
                new UseOfCFDI(14, "D03", "D03", "Gastos funerales."),
                new UseOfCFDI(15, "D04", "D04", "Donativos"),
                new UseOfCFDI(16, "D05", "D05", "Intereses reales efectivamente pagados por créditos hipotecarios (casa habitación)."),
                new UseOfCFDI(17, "D06", "D06", "Aportaciones voluntarias al SAR."),
                new UseOfCFDI(18, "D07", "D07", "Primas por seguros de gastos médicos."),
                new UseOfCFDI(19, "D08", "D08", "Gastos de transportación escolar obligatoria."),
                new UseOfCFDI(20, "D09", "D09", "\tDepósitos en cuentas para el ahorro, primas que tengan como base planes de pensiones."),
                new UseOfCFDI(21, "D10", "D10", "Pagos por servicios educativos (colegiaturas)"),
                new UseOfCFDI(22, "CP01", "CP01", "Pagos"),
                new UseOfCFDI(23, "CN01", "CN01", "Nómina"),
                new UseOfCFDI(24, "S01", "S01", "Sin Efectos Fiscales")
            };

            return cfdis;
        }

        private static List<Department> GetDepartments()
        {
            var path = "wwwroot/seed/CAT_DEPARTAMENTOS.xlsx";
            var tableData = ReadAsTable(path);

            var departments = tableData.AsEnumerable().Select(x => new Department(
                Convert.ToInt32(x.Field<double>("Id")),
                x.Field<string>("Clave"),
                x.Field<string>("Nombre") ?? ""
                )).ToList();

            return departments;
        }

        private static List<Area> GetAreas()
        {
            var path = "wwwroot/seed/CAT_Areas.xlsx";
            var tableData = ReadAsTable(path);

            var departments = GetDepartments();

            var areas = tableData.AsEnumerable().Select(x =>
            {
                var department = departments.First(y => y.Clave == x.Field<string>("Departamento"));

                return new Area(
                    Convert.ToInt32(x.Field<double>("Id")),
                    department.Id,
                    x.Field<string>("Clave"),
                    x.Field<string>("Nombre") ?? "");
            }).ToList();

            return areas;
        }

        private static List<Maquila> GetMaquilas()
        {
            var path = "wwwroot/seed/CAT_MAQUILADORES.xlsx";
            var tableData = ReadAsTable(path);

            var maquilas = tableData.AsEnumerable().Select(x => new Maquila(
                Convert.ToInt32(x.Field<double>("Id")),
                x.Field<string>("Clave"),
                x.Field<string>("Nombre") ?? ""
                )).ToList();

            return maquilas;
        }

        private static List<Indication> GetIndications()
        {
            var path = "wwwroot/seed/CAT_INDICACIONES.xlsx";
            var tableData = ReadAsTable(path);

            var maquilas = tableData.AsEnumerable().Select(x => new Indication(
                Convert.ToInt32(x.Field<double>("Id")),
                x.Field<string>("Clave"),
                x.Field<string>("Nombre") ?? ""
                )).ToList();

            return maquilas;
        }

        private static List<Tapon> GetTags()
        {
            var path = "wwwroot/seed/CAT_ETIQUETAS.xlsx";
            var tableData = ReadAsTable(path);

            var tags = tableData.AsEnumerable().Select(x => new Tapon(
                Convert.ToInt32(x.Field<double>("Id")),
                x.Field<string>("Clave"),
                x.Field<string>("Nombre"),
                x.Field<string>("Color")
                )).ToList();

            return tags;
        }

        private static List<Parameter> GetParameters()
        {
            var path = "wwwroot/seed/CAT_PARAMETROS.xlsx";
            var tableData = ReadAsTable(path);

            var areas = GetAreas();

            var parameters = tableData.AsEnumerable().Select(x =>
            {
                var area = areas.FirstOrDefault(a => a.Clave == x.Field<string>("Area"));
                var type = GetValueType(x.Field<string>("TipoR"));

                return new Parameter(
                    Guid.Parse(x.Field<string>("Id")),
                    x.Field<string>("Clave"),
                    x.Field<string>("Nombre"),
                    x.Field<string>("Corto"),
                    type,
                    x.Field<string>("Formula"),
                    area?.Id ?? 0,
                    area?.DepartamentoId ?? 0,
                    1,
                    1,
                    x.Field<string>("Fcsi"));
            }).ToList();

            return parameters;
        }

        // Guid id, string clave, string nombre, string nombreCorto, string tipoValor, string formula, int areaId, int departamentoId, int unidadId, int unidadSiId, string fcsi

        private static List<Study> GetStudies()
        {
            var path = "wwwroot/seed/CAT_ESTUDIOS.xlsx";
            var tableData = ReadAsTable(path);

            var areas = GetAreas();
            var maquilas = GetMaquilas();
            var methods = GetMethods();
            var tags = GetTags();

            var tagsR = ReadAsTable("wwwroot/seed/CAT_ESTUDIOS_ETIQ.xlsx").AsEnumerable();

            var studies = tableData.AsEnumerable().Select(x =>
            {
                var area = areas.FirstOrDefault(a => a.Clave == x.Field<string>("Area"));
                var maquila = maquilas.FirstOrDefault(m => m.Clave == x.Field<string>("Maquilador"));
                var method = methods.FirstOrDefault(m => m.Clave == x.Field<string>("Metodo"));
                var tagR = tagsR.FirstOrDefault(t => t.Field<string>("ClaveEstudio") == x.Field<string>("Clave"));
                var tag = tags.FirstOrDefault(t => t.Clave == tagR?.Field<string>("ClaveEtiqueta"));

                return new Study(
                    Convert.ToInt32(x.Field<double>("Id")),
                    x.Field<string>("Clave"),
                    x.Field<string>("Nombre"),
                    Convert.ToInt32(x.Field<double>("Orden")),
                    x.Field<string>("Titulo"),
                    x.Field<string>("Corto"),
                    x.Field<string>("Visible") == "V",
                    Convert.ToInt32(x.Field<double>("Dias")),
                    area?.Id ?? 0,
                    area?.DepartamentoId ?? 0,
                    maquila?.Id ?? 0,
                    method?.Id ?? 0,
                    tag?.Id ?? 0);
            }).ToList();

            return studies;
        }

        private static List<IndicationStudy> GetStudiesIndications()
        {
            var path = "wwwroot/seed/CAT_ESTUDIOS_INDI.xlsx";
            var tableData = ReadAsTable(path);

            var indications = GetIndications();
            var studies = GetStudies();

            var studyIndications = tableData.AsEnumerable().Select(x =>
            {
                var study = studies.FirstOrDefault(s => s.Clave == x.Field<string>("ClaveEstudio"));
                var indication = indications.FirstOrDefault(i => i.Clave == x.Field<string>("ClaveIndicacion"));

                return new IndicationStudy(
                    indication?.Id ?? 0,
                    study?.Id ?? 0);
            }).ToList();

            studyIndications = studyIndications.Where(x => x.IndicacionId > 0 && x.EstudioId > 0).ToList();

            return studyIndications;
        }

        private static List<Branch> GetBranches()
        {
            var branches = new List<Branch>
            {
                new Branch(BR.MT,           "MT",            "Matriz",                   1, "110000-110999", "644-415-16-92", "laboratorioramos.recepcion@gmail.com",   "Sinaloa",                      "144",  null,   "85000", 123661, "Cajeme",                   "Sonora"),
                new Branch(BR.CMSS,         "CMSS",          "Centro Médico Sur Sonora", 0, "111000-111299", "644-104-22-30", "labramoscmss@gmail.com",                 "Norte",                        null,   "749",  "85040", 123705, "Cajeme",                   "Sonora"),
                new Branch(BR.U200,         "U200",          "Unidad 200",               0, "111300-111599", "644-416-14-07", "labramos200@gmail.com",                  "200",                          null,   null,   "85150", 123819, "Cajeme",                   "Sonora"),
                new Branch(BR.U300,         "U300",          "Unidad 300",               0, "111600-111899", "644-444-66-69", "labramos300@gmail.com",                  "Jalisco",                      null,   "2250", "85080", 123739, "Cajeme",                   "Sonora"),
                new Branch(BR.ALAMEDA,      "ALAMEDA",       "Unidad Alameda",           0, "111900-112199", "644-418-01-44", "labramoscalifornia@gmail.com",           "California",                   "358",  null,   "85219", 125349, "Cajeme",                   "Sonora"),
                new Branch(BR.HACIENDAS,    "HACIENDAS",     "Unidad Las Haciendas",     0, "112200-112499", "644-104-22-30", "labramoshaciendas@gmail.com",            "No Reelección",                "2425", "1",    "85064", 123730, "Cajeme",                   "Sonora"),
                new Branch(BR.REFORMA,      "REFORMA",       "Unidad Reforma",           0, "131600-131899", "662-213-68-66", "labramosreforma@gmail.com",              "Reforma",                      "273",  null,   "83270", 119437, "Hermosillo",               "Sonora"),
                new Branch(BR.MORELOS,      "MORELOS",       "Unidad Morelos",           0, "131300-131599", "662-267-86-35", "lab.ramos.morelos@hotmail.com",          "Blvd. José María Morelos",     "357",  null,   "83144", 119192, "Hermosillo",               "Sonora"),
                new Branch(BR.SOLIDARIDAD,  "SOLIDARIDAD",   "Unidad Solidaridad",       1, "130000-130999", "662-216-41-38", "solidaridad.labramos@gmail.com",         "Solidaridad 2",                null,   null,   "83200", 119328, "Hermosillo",               "Sonora"),
                new Branch(BR.MNORTE,       "MNORTE",        "Unidad Médica Norte",      0, "131000-131299", "662-118-76-67", "labramos.medicanorte@gmail.com",         "Blvd. Solidaridad",            "574",  null,   "83110", 119063, "Hermosillo",               "Sonora"),
                new Branch(BR.CANTABRIA,    "CANTABRIA",     "Unidad Cantabria",         0, "131900-132199", "662-980-04-16", "labramos.cantabria@gmail.com ",          "Blvd. Colosio",                "803",  "14",   "83224", 119360, "Hermosillo",               "Sonora"),
                new Branch(BR.UNIDAD,       "UNIDAD",        "Unidad Guaymas",           1, "150001-150999", "622-221-91-83", "labramos.guaymas@gmail.com",             "Av. Calzada A. García López",  null,   null,   "85440", 126411, "Heroica Guaymas",          "Sonora"),
                new Branch(BR.NAVOJOA,      "NAVOJOA",       "Navojoa Talamante",        1, "120001-120999", "642-421-19-41", "lramos.navojoa@gmail.com",               "Talamante",                    "703",  null,   "85870", 127452, "Navojoa",                  "Sonora"),
                new Branch(BR.NAVOJOA2,     "NAVOJOA2",      "Navojoa Quintana Roo",     0, "121000-121299", "642-120-01-44", "lramos.navojoa2@gmail.com",              "Av. Quintana Roo Oriente",     "106",  null,   "85870", 127452, "Navojoa",                  "Sonora"),
                new Branch(BR.KENNEDY,      "KENNEDY",       "Nogales Kennedy",          1, "140000-140999", "631-690-18-30", "labramos.nogales@gmail.com",             "Av. Kennedy",                  "156",  null,   "84066", 121756, "Nogales",                  "Sonora"),
                new Branch(BR.NOGALES1,     "NOGALES1",      "Nogales Álvaro Obregón",   0, "141000-141299", "631-209-91-39", "labramos.nogales@gmail.com",             "Av. Álvaro Obregón",           "1623", null,   "84055", 121715, "Nogales",                  "Sonora"),
                new Branch(BR.CUMBRES,      "CUMBRES",       "Cumbres, Monterrey",       1, "210000-210999", "818-526-00-22", "labramos.mty@gmail.com",                 "Av. Paseo de los Leones",      "2301", null,   "64610",  87256, "Monterrey",                "Nuevo León"),
                new Branch(BR.SPGG,         "SPGG",          "San Pedro Garza García",   1, "220000-220999", "814-170-07-69", "labramos.cumbres@gmail.com",             "Av. Humberto Lobo",            "555A", null,   "66220",  88801, "San Pedro Garza García",   "Nuevo León" ),
            };

            return branches;
        }

        public static DataTable ReadAsTable(string filePath)
        {
            using var wb = new XLWorkbook(filePath, XLEventTracking.Disabled);
            var ws = wb.Worksheet(1);
            DataTable dataTable = ws.RangeUsed().AsTable().AsNativeDataTable();
            return dataTable;
        }

        private static string GetValueType(string type)
        {
            return type switch
            {
                "N" => "1",
                "NS" => "2",
                "NR" => "3",
                "NSR" => "4",
                "M" => "5",
                "NT" => "6",
                "NT2" => "6",
                "NT3" => "6",
                "NT4" => "6",
                "T" => "7",
                "P" => "8",
                "L" => "9",
                "O" => "10",
                _ => "0",
            };
        }
    }
}
