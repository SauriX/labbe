﻿using EFCore.BulkExtensions;
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
using ValueTypes = Shared.Dictionary.Catalogs.ValueType;
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
using Service.Catalog.Domain.Route;

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
            }

            // SUCURSALES
            if (!context.CAT_Sucursal.Any())
            {
                var branches = SeedData.SeedData.GetBranches();

                context.CAT_Sucursal.AddRange(branches);

                await context.SaveChangesAsync();
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
            }
            // Notifications
            if (!context.Cat_notificaciones.Any())
            {
                var notifications = SeedData.SeedData.GetNotifications();

                context.Cat_notificaciones.AddRange(notifications);

                await context.SaveChangesAsync();
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
            }

            // ETIQUETAS
            if (!context.CAT_Etiqueta.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var caps = SeedData.SeedData.GetTags();

                    context.CAT_Etiqueta.AddRange(caps);

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Etiqueta)} ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Etiqueta)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            if (!context.CAT_Etiqueta.Any(x => !string.IsNullOrEmpty(x.ClaveInicial)))
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var caps = SeedData.SeedData.GetTags();

                    await context.BulkUpdateAsync(caps);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
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
            }

            // PARAMETROS
            if (!context.CAT_Parametro.Any())
            {
                var parameters = SeedData.SeedData.GetParameters();

                context.CAT_Parametro.AddRange(parameters);

                await context.SaveChangesAsync();
            }

            // ESTUDIOS
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
            }

            // PARAMETROS VALORES
            if (!context.CAT_Tipo_Valor.Any())
            {
                var parameterValues = SeedData.SeedData.GetParameterValues();

                context.CAT_Tipo_Valor.AddRange(parameterValues);

                await context.SaveChangesAsync();
            }

            // ESTUDIOS ETIQUETAS
            if (!context.Relacion_Estudio_Etiqueta.Any())
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var caps = SeedData.SeedData.GetStudyTags();

                    context.Relacion_Estudio_Etiqueta.AddRange(caps);

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.Relacion_Estudio_Etiqueta)} ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.Relacion_Estudio_Etiqueta)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // ESTUDIOS INDICACIONES
            if (!context.Relacion_Estudio_Indicacion.Any())
            {
                var studyIndications = SeedData.SeedData.GetStudyIndications();

                context.Relacion_Estudio_Indicacion.AddRange(studyIndications);

                await context.SaveChangesAsync();
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
            }

            // PAQUETES ESTUDIOS
            if (!context.Relacion_Estudio_Paquete.Any())
            {
                var packStudies = SeedData.SeedData.GetPackStudies();

                context.Relacion_Estudio_Paquete.AddRange(packStudies);

                await context.SaveChangesAsync();
            }

            // PRECIOS ESTUDIOS
            if (!context.Relacion_ListaP_Estudio.Any())
            {
                var studyPrices = SeedData.SeedData.GetStudyPrices();

                context.Relacion_ListaP_Estudio.AddRange(studyPrices);

                await context.SaveChangesAsync();
            }

            // RUTAS
            if (!context.CAT_Rutas.Any())
            {
                var studies = await context.CAT_Estudio.Include(x => x.Maquilador).Where(x => x.MaquiladorId != null).ToListAsync();
                var branches = await context.CAT_Sucursal.ToListAsync();

                var routes = new List<Route>();

                foreach (var branch in branches)
                {
                    foreach (var group in studies.GroupBy(x => new { x.MaquiladorId, x.Maquilador.Clave, x.Maquilador.Nombre }))
                    {
                        routes.Add(new Route(
                            $"R-{branch.Clave}-{group.Key.Clave}",
                            $"Ruta {branch.Nombre} - {group.Key.Nombre}",
                            branch.Id,
                            (int)group.Key.MaquiladorId,
                            group.Select(x => x.Id)));
                    }
                }

                context.CAT_Rutas.AddRange(routes);

                await context.SaveChangesAsync();
            }
        }
    }
}
