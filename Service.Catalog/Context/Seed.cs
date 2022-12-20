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
using PL = Shared.Dictionary.Catalogs.PriceList;
using DEP = Shared.Dictionary.Catalogs.Department;
using AREAS = Shared.Dictionary.Catalogs.Area;
using BR = Shared.Dictionary.Catalogs.Branch;
using ValueTypes = Shared.Dictionary.Catalogs.TipoValor;
using Shared.Utils;
using ClosedXML.Excel;
using System.Data;
using Service.Catalog.Domain.Maquila;
using Service.Catalog.Domain.Indication;
using Service.Catalog.Domain.Tapon;
using Service.Catalog.Domain.Study;
using RabbitMQ.Client;
using Service.Catalog.Domain.Parameter;
using Service.Catalog.Domain.Packet;
using System.Net;
using Shared.Error;
using Shared.Extensions;
using Service.Catalog.Domain.Price;
using Shared.Dictionary;
using Service.Catalog.Domain.Company;

namespace Service.Catalog.Context
{
    public class Seed
    {
        public static async Task SeedTables(ApplicationDbContext context, bool update)
        {
            // PROCEDENCIA
            if (!context.CAT_Procedencia.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var origins = SeedData.SeedData.GetOrigins();

                    context.CAT_Procedencia.AddRange(origins);

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Procedencia)} ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Procedencia)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // CCOMPAÑIA DEFAULT
            if (!context.CAT_Compañia.Any())
            {
                var company = SeedData.SeedData.GetDefaultCompany();

                context.CAT_Compañia.Add(company);

                await context.SaveChangesAsync();
            }

            // MEDICO DEFAULT
            if (!context.CAT_Medicos.Any())
            {
                var medic = SeedData.SeedData.GetDefaultMedic();

                context.CAT_Medicos.Add(medic);

                await context.SaveChangesAsync();
            }

            // CONFIGURACION
            if (!context.CAT_Configuracion.Any())
            {
                var configuration = SeedData.SeedData.GetConfiguration();

                context.CAT_Configuracion.AddRange(configuration);

                await context.SaveChangesAsync();

                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var configuration = SeedData.SeedData.GetConfiguration();

                //    context.CAT_Configuracion.AddRange(configuration);

                //    await context.SaveChangesAsync();

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // FORMAS DE PAGO
            if (!context.CAT_FormaPago.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var payments = SeedData.SeedData.GetPaymentForms();

                    context.CAT_FormaPago.AddRange(payments);

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_FormaPago)} ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_FormaPago)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var payments = SeedData.SeedData.GetPaymentForms();
                //    var payment = new Payment();

                //    var script = MergeGenerator.Build(
                //        nameof(context.CAT_FormaPago),
                //        payments,
                //        nameof(payment.Id),
                //        nameof(payment.Clave),
                //        nameof(payment.Nombre),
                //        nameof(payment.Descripcion),
                //        nameof(payment.Activo));

                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_FormaPago)} ON;");
                //    context.Database.ExecuteSqlRaw(script);
                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_FormaPago)} OFF;");

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // USOS DE CFDI
            if (!context.CAT_CFDI.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var uses = SeedData.SeedData.GetUseOfCFDIs();

                    context.CAT_CFDI.AddRange(uses);

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_CFDI)} ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_CFDI)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var cfdis = SeedData.SeedData.GetUseOfCFDIs();
                //    var cdfi = new UseOfCFDI();

                //    var script = MergeGenerator.Build(
                //        nameof(context.CAT_CFDI),
                //        cfdis,
                //        nameof(cdfi.Id),
                //        nameof(cdfi.Clave),
                //        nameof(cdfi.Nombre),
                //        nameof(cdfi.Descripcion),
                //        nameof(cdfi.Activo));

                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_CFDI)} ON;");
                //    context.Database.ExecuteSqlRaw(script);
                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_CFDI)} OFF;");

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // SUCURSALES
            if (!context.CAT_Sucursal.Any())
            {
                var branches = SeedData.SeedData.GetBranches();

                context.CAT_Sucursal.AddRange(branches);

                await context.SaveChangesAsync();
                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var branches = SeedData.SeedData.GetBranches();
                //    var branch = new Branch();

                //    var script = MergeGenerator.Build(
                //        nameof(context.CAT_Sucursal),
                //        branches,
                //        nameof(branch.Id),
                //        nameof(branch.Codigo),
                //        nameof(branch.Clave),
                //        nameof(branch.Nombre),
                //        nameof(branch.Clinicos),
                //        nameof(branch.Telefono),
                //        nameof(branch.Correo),
                //        nameof(branch.Calle),
                //        nameof(branch.NumeroExterior),
                //        nameof(branch.NumeroInterior),
                //        nameof(branch.Codigopostal),
                //        nameof(branch.ColoniaId),
                //        nameof(branch.Ciudad),
                //        nameof(branch.Estado),
                //        nameof(branch.Matriz),
                //        nameof(branch.Activo));

                //    context.Database.ExecuteSqlRaw(script);

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // LISTA DE PRECIOS DEFAULT
            if (!context.CAT_ListaPrecio.Any())
            {
                var priceList = SeedData.SeedData.GetDefaultPriceList();

                context.CAT_ListaPrecio.Add(priceList);

                await context.SaveChangesAsync();
            }

            // SUCURSALES CONFIGURACION FOLIO
            if (!context.CAT_Sucursal_Folio.Any())
            {
                var configuration = SeedData.SeedData.GetClinicosConfig();

                context.CAT_Sucursal_Folio.AddRange(configuration);

                await context.SaveChangesAsync();

                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var ciudades = new List<BranchFolioConfig>()
                //    {
                //        // Sonora -> Ciudad Obregon
                //        new BranchFolioConfig(26, 2059, 1, 1),    
                //        // Sonora -> Navojoa
                //        new BranchFolioConfig(26, 2042, 1, 2),  
                //        // Sonora -> Hermosillo
                //        new BranchFolioConfig(26, 2045, 1, 3),
                //        // Sonora -> Heroica Nogales
                //        new BranchFolioConfig(26, 2066, 1, 4),
                //        // Sonora -> Heroica Guaymas
                //        new BranchFolioConfig(26, 2041, 1, 5),
                //        // Nuevo León -> Monterrey
                //        new BranchFolioConfig(19, 1061, 2, 1),
                //        // Nuevo León -> San Pedro
                //        new BranchFolioConfig(19, 1073, 2, 2),
                //};

                //    context.CAT_Sucursal_Folio.AddRange(ciudades);
                //    await context.SaveChangesAsync();

                //    transaction.Commit();

                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // DEPARTAMENTOS
            if (!context.CAT_Departamento.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var departments = SeedData.SeedData.GetDepartments();

                    context.CAT_Departamento.AddRange(departments);

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Departamento)} ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Departamento)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var departments = SeedData.SeedData.GetDepartments();
                //    var department = new Department();

                //    var script = MergeGenerator.Build(
                //        nameof(context.CAT_Departamento),
                //        departments,
                //        nameof(department.Id),
                //        nameof(department.Clave),
                //        nameof(department.Nombre),
                //        nameof(department.Activo));

                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Departamento)} ON;");
                //    context.Database.ExecuteSqlRaw(script);
                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Departamento)} OFF;");

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // AREAS
            if (!context.CAT_Area.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var areas = SeedData.SeedData.GetAreas();

                    context.CAT_Area.AddRange(areas);

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Area)} ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Area)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var areas = SeedData.SeedData.GetAreas();
                //    var area = new Area();

                //    var script = MergeGenerator.Build(
                //        nameof(context.CAT_Area),
                //        areas,
                //        nameof(area.Id),
                //        nameof(area.DepartamentoId),
                //        nameof(area.Clave),
                //        nameof(area.Nombre),
                //        nameof(area.Activo));

                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Area)} ON;");
                //    context.Database.ExecuteSqlRaw(script);
                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Area)} OFF;");

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // ETIQUETAS
            if (!context.CAT_Tipo_Tapon.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var caps = SeedData.SeedData.GetTags();

                    context.CAT_Tipo_Tapon.AddRange(caps);

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Tipo_Tapon)} ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Tipo_Tapon)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var tags = SeedData.SeedData.GetTags();
                //    var tag = new Tapon();

                //    var script = MergeGenerator.Build(
                //        nameof(context.CAT_Tipo_Tapon),
                //        tags,
                //        nameof(tag.Id),
                //        nameof(tag.Clave),
                //        nameof(tag.Nombre),
                //        nameof(tag.Color));

                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Tipo_Tapon)} ON;");
                //    context.Database.ExecuteSqlRaw(script);
                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Tipo_Tapon)} OFF;");

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // INDICACIONES
            if (!context.CAT_Indicacion.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var indications = SeedData.SeedData.GetIndications();

                    context.CAT_Indicacion.AddRange(indications);

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Indicacion)} ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Indicacion)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var indications = SeedData.SeedData.GetIndications();
                //    var indication = new Indication();

                //    var script = MergeGenerator.Build(
                //        nameof(context.CAT_Indicacion),
                //        indications,
                //        nameof(indication.Id),
                //        nameof(indication.Clave),
                //        nameof(indication.Nombre),
                //        nameof(indication.Descripcion),
                //        nameof(indication.Activo));

                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Indicacion)} ON;");
                //    context.Database.ExecuteSqlRaw(script);
                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Indicacion)} OFF;");

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // METODOS
            if (!context.CAT_Metodo.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var methods = SeedData.SeedData.GetMethods();

                    context.CAT_Metodo.AddRange(methods);

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Metodo)} ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Metodo)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var methods = SeedData.SeedData.GetMethods();
                //    var method = new Method();

                //    var script = MergeGenerator.Build(
                //        nameof(context.CAT_Metodo),
                //        methods,
                //        nameof(method.Id),
                //        nameof(method.Clave),
                //        nameof(method.Nombre),
                //        nameof(method.Activo));

                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Metodo)} ON;");
                //    context.Database.ExecuteSqlRaw(script);
                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Metodo)} OFF;");

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // MAQUILADORES
            if (!context.CAT_Maquilador.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var maquilas = SeedData.SeedData.GetMaquilas();

                    context.CAT_Maquilador.AddRange(maquilas);

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Maquilador)} ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Maquilador)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var maquilas = SeedData.SeedData.GetMaquilas();
                //    var maquila = new Maquila();

                //    var script = MergeGenerator.Build(
                //        nameof(context.CAT_Maquilador),
                //        maquilas,
                //        nameof(maquila.Id),
                //        nameof(maquila.Clave),
                //        nameof(maquila.Nombre),
                //        nameof(maquila.Activo));

                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Maquilador)} ON;");
                //    context.Database.ExecuteSqlRaw(script);
                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Maquilador)} OFF;");

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // UNIDADES
            if (!context.CAT_Units.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var units = SeedData.SeedData.GetUnits();

                    context.CAT_Units.AddRange(units);

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Units)} ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Units)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var units = SeedData.SeedData.GetUnits();
                //    var unit = new Units();

                //    var script = MergeGenerator.Build(
                //        nameof(context.CAT_Units),
                //        units,
                //        nameof(unit.Id),
                //        nameof(unit.Clave),
                //        nameof(unit.Activo));

                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Units)} ON;");
                //    context.Database.ExecuteSqlRaw(script);
                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Units)} OFF;");

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // PARAMETROS
            if (!context.CAT_Parametro.Any())
            {
                var parameters = SeedData.SeedData.GetParameters();

                context.CAT_Parametro.AddRange(parameters);

                await context.SaveChangesAsync();

                //context.CAT_Parametro.AddRange(parameters);

                //await context.SaveChangesAsync();

                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var parameters = SeedData.SeedData.GetParameters(context);
                //    var parameter = new Parameter();

                //    var script = MergeGenerator.Build(
                //        nameof(context.CAT_Parametro),
                //        parameters,
                //        nameof(parameter.Id),
                //        nameof(parameter.Clave),
                //        nameof(parameter.Nombre),
                //        nameof(parameter.NombreCorto),
                //        nameof(parameter.TipoValor),
                //        nameof(parameter.Formula),
                //        nameof(parameter.AreaId),
                //        nameof(parameter.DepartamentoId),
                //        nameof(parameter.UnidadId),
                //        nameof(parameter.UnidadSiId),
                //        nameof(parameter.FCSI),
                //        nameof(parameter.Activo));

                //    //context.Database.ExecuteSqlRaw(script);

                //    context.BulkInsertOrUpdate(parameters);

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // Studies
            if (!context.CAT_Estudio.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var studies = SeedData.SeedData.GetStudies();

                    context.CAT_Estudio.AddRange(studies);

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Estudio)} ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Estudio)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var studies = SeedData.SeedData.GetStudies(context);
                //    var study = new Study();

                //    var script = MergeGenerator.Build(
                //        nameof(context.CAT_Estudio),
                //        studies,
                //        nameof(study.Id),
                //        nameof(study.Clave),
                //        nameof(study.Nombre),
                //        nameof(study.NombreCorto),
                //        nameof(study.Orden),
                //        nameof(study.Titulo),
                //        nameof(study.AreaId),
                //        nameof(study.DepartamentoId),
                //        nameof(study.Visible),
                //        nameof(study.DiasResultado),
                //        nameof(study.Dias),
                //        nameof(study.TiempoResultado),
                //        nameof(study.MaquiladorId),
                //        nameof(study.MetodoId),
                //        nameof(study.TaponId),
                //        nameof(study.Cantidad),
                //        nameof(study.Prioridad),
                //        nameof(study.Urgencia),
                //        nameof(study.Activo));

                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Estudio)} ON;");
                //    context.Database.ExecuteSqlRaw(script);
                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Estudio)} OFF;");

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // PAQUETES
            if (!context.CAT_Paquete.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var packs = SeedData.SeedData.GetPacks();

                    context.CAT_Paquete.AddRange(packs);

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Paquete)} ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Paquete)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var packs = SeedData.SeedData.GetPacks();
                //    var pack = new Packet();

                //    var script = MergeGenerator.Build(
                //        nameof(context.CAT_Paquete),
                //        packs,
                //        nameof(pack.Id),
                //        nameof(pack.Clave),
                //        nameof(pack.Nombre),
                //        nameof(pack.NombreLargo),
                //        nameof(pack.AreaId),
                //        nameof(pack.DepartamentoId),
                //        nameof(pack.Visibilidad),
                //        nameof(pack.Activo));

                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Paquete)} ON;");
                //    context.Database.ExecuteSqlRaw(script);
                //    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Paquete)} OFF;");

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // PARAMETROS VALORES
            if (!context.CAT_Tipo_Valor.Any())
            {
                var parameterValues = SeedData.SeedData.GetParameterValues();

                context.CAT_Tipo_Valor.AddRange(parameterValues);

                await context.SaveChangesAsync();

                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var parameterValues = SeedData.SeedData.GetParameterValues(context);

                //    context.BulkInsertOrUpdate(parameterValues);

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // ESTUDIOS INDICACIONES
            if (!context.Relacion_Estudio_Indicacion.Any())
            {
                var studyIndications = SeedData.SeedData.GetStudyIndications();

                context.Relacion_Estudio_Indicacion.AddRange(studyIndications);

                await context.SaveChangesAsync();

                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var studyIndications = SeedData.SeedData.GetStudyIndications(context);
                //    var si = new IndicationStudy();

                //    var script = MergeGenerator.Build(
                //        nameof(context.Relacion_Estudio_Indicacion),
                //        studyIndications,
                //        new string[] { nameof(si.EstudioId), nameof(si.IndicacionId) },
                //        nameof(si.Activo));

                //    //context.Database.ExecuteSqlRaw(script);
                //    context.BulkInsertOrUpdate(studyIndications);

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // ESTUDIOS PARAMETROS
            if (!context.Relacion_Estudio_Parametro.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var studyParameters = SeedData.SeedData.GetStudyParameters();

                    context.Relacion_Estudio_Parametro.AddRange(studyParameters);

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.Relacion_Estudio_Parametro)} ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.Relacion_Estudio_Parametro)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }



                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var studyParameters = SeedData.SeedData.GetStudyParameters(context);
                //    var sp = new ParameterStudy();

                //    var script = MergeGenerator.Build(
                //        nameof(context.Relacion_Estudio_Parametro),
                //        studyParameters,
                //        new string[] { nameof(sp.EstudioId), nameof(sp.ParametroId) },
                //        nameof(sp.Activo));

                //    //context.Database.ExecuteSqlRaw(script);
                //    context.BulkInsertOrUpdate(studyParameters);

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // PAQUETES ESTUDIOS
            if (!context.Relacion_Estudio_Paquete.Any())
            {
                var packStudies = SeedData.SeedData.GetPackStudies();

                context.Relacion_Estudio_Paquete.AddRange(packStudies);

                await context.SaveChangesAsync();

                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var packStudies = SeedData.SeedData.GetPackStudies(context);
                //    var ps = new PacketStudy();

                //    var script = MergeGenerator.Build(
                //        nameof(context.Relacion_Estudio_Paquete),
                //        packStudies,
                //        new string[] { nameof(ps.PacketId), nameof(ps.EstudioId) },
                //        nameof(ps.Activo));

                //    //context.Database.ExecuteSqlRaw(script);
                //    context.BulkInsertOrUpdate(packStudies);

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }

            // PRECIOS ESTUDIOS
            if (!context.Relacion_ListaP_Estudio.Any())
            {
                var studyPrices = SeedData.SeedData.GetStudyPrices();

                context.Relacion_ListaP_Estudio.AddRange(studyPrices);

                await context.SaveChangesAsync();

                //using var transaction = context.Database.BeginTransaction();

                //try
                //{
                //    var packStudies = SeedData.SeedData.GetPackStudies(context);
                //    var ps = new PacketStudy();

                //    var script = MergeGenerator.Build(
                //        nameof(context.Relacion_Estudio_Paquete),
                //        packStudies,
                //        new string[] { nameof(ps.PacketId), nameof(ps.EstudioId) },
                //        nameof(ps.Activo));

                //    //context.Database.ExecuteSqlRaw(script);
                //    context.BulkInsertOrUpdate(packStudies);

                //    transaction.Commit();
                //}
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
            }
        }
    }
}
