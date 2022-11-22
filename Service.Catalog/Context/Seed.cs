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
using AREAS = Shared.Dictionary.Catalogs.Area;
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
using Service.Catalog.Domain.Packet;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Service.Catalog.Domain.Medics;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace Service.Catalog.Context
{
    public class Seed
    {
        public static async Task SeedData(ApplicationDbContext context, bool update)
        {
            if (!context.CAT_Configuracion.Any())
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var configuration = new List<Configuration>()
                    {
                        new Configuration(1, "Correo"),
                        new Configuration(2, "Remitente"),
                        new Configuration(3,"SMTP" ),
                        new Configuration(4,"Requiere Contraseña"),
                        new Configuration(5,"Contraseña"),
                        new Configuration(6,"Nombre Sistema"),
                        new Configuration(7,"Logo","logo.png"),
                        new Configuration(8,"RFC"),
                        new Configuration(9,"Razón Social"),
                        new Configuration(10,"CP"),
                        new Configuration(11,"Estado"),
                        new Configuration(12,"Colonia"),
                        new Configuration(13,"Calle"),
                        new Configuration(14,"Número"),
                        new Configuration(15,"Teléfono"),
                        new Configuration(16,"Ciudad")
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

            // Payment
            if (update)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var payments = GetPaymentForms();
                    var payment = new Payment();

                    var script = MergeGenerator.Build(
                        nameof(context.CAT_FormaPago),
                        payments,
                        nameof(payment.Id),
                        nameof(payment.Clave),
                        nameof(payment.Nombre),
                        nameof(payment.Descripcion),
                        nameof(payment.Activo));

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_FormaPago)} ON;");
                    context.Database.ExecuteSqlRaw(script);
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_FormaPago)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // UsesOfCfdi
            if (update)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var cfdis = GetUseOfCFDIs();
                    var cdfi = new UseOfCFDI();

                    var script = MergeGenerator.Build(
                        nameof(context.CAT_CFDI),
                        cfdis,
                        nameof(cdfi.Id),
                        nameof(cdfi.Clave),
                        nameof(cdfi.Nombre),
                        nameof(cdfi.Descripcion),
                        nameof(cdfi.Activo));

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_CFDI)} ON;");
                    context.Database.ExecuteSqlRaw(script);
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_CFDI)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // Methods
            if (update)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var methods = GetMethods();
                    var method = new Method();

                    var script = MergeGenerator.Build(
                        nameof(context.CAT_Metodo),
                        methods,
                        nameof(method.Id),
                        nameof(method.Clave),
                        nameof(method.Nombre),
                        nameof(method.Activo));

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Metodo)} ON;");
                    context.Database.ExecuteSqlRaw(script);
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Metodo)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // Departments
            if (update)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var departments = GetDepartments();
                    var department = new Department();

                    var script = MergeGenerator.Build(
                        nameof(context.CAT_Departamento),
                        departments,
                        nameof(department.Id),
                        nameof(department.Clave),
                        nameof(department.Nombre),
                        nameof(department.Activo));

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Departamento)} ON;");
                    context.Database.ExecuteSqlRaw(script);
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Departamento)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // Areas
            if (update)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var areas = GetAreas();
                    var area = new Area();

                    var script = MergeGenerator.Build(
                        nameof(context.CAT_Area),
                        areas,
                        nameof(area.Id),
                        nameof(area.DepartamentoId),
                        nameof(area.Clave),
                        nameof(area.Nombre),
                        nameof(area.Activo));

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Area)} ON;");
                    context.Database.ExecuteSqlRaw(script);
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Area)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // Indications
            if (update)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var indications = GetIndications();
                    var indication = new Indication();

                    var script = MergeGenerator.Build(
                        nameof(context.CAT_Indicacion),
                        indications,
                        nameof(indication.Id),
                        nameof(indication.Clave),
                        nameof(indication.Nombre),
                        nameof(indication.Descripcion),
                        nameof(indication.Activo));

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Indicacion)} ON;");
                    context.Database.ExecuteSqlRaw(script);
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Indicacion)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // Tags
            if (update)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var tags = GetTags();
                    var tag = new Tapon();

                    var script = MergeGenerator.Build(
                        nameof(context.CAT_Tipo_Tapon),
                        tags,
                        nameof(tag.Id),
                        nameof(tag.Clave),
                        nameof(tag.Nombre),
                        nameof(tag.Color));

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Tipo_Tapon)} ON;");
                    context.Database.ExecuteSqlRaw(script);
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Tipo_Tapon)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // Units
            if (update)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var units = GetUnits();
                    var unit = new Units();

                    var script = MergeGenerator.Build(
                        nameof(context.CAT_Units),
                        units,
                        nameof(unit.Id),
                        nameof(unit.Clave),
                        nameof(unit.Activo));

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Units)} ON;");
                    context.Database.ExecuteSqlRaw(script);
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Units)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // Maquilas
            if (update)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var maquilas = GetMaquilas();
                    var maquila = new Maquila();

                    var script = MergeGenerator.Build(
                        nameof(context.CAT_Maquilador),
                        maquilas,
                        nameof(maquila.Id),
                        nameof(maquila.Clave),
                        nameof(maquila.Nombre),
                        nameof(maquila.Activo));

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Maquilador)} ON;");
                    context.Database.ExecuteSqlRaw(script);
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Maquilador)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // Branches
            if (update)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var branches = GetBranches();
                    var branch = new Branch();

                    var script = MergeGenerator.Build(
                        nameof(context.CAT_Sucursal),
                        branches,
                        nameof(branch.Id),
                        nameof(branch.Clave),
                        nameof(branch.Nombre),
                        nameof(branch.Clinicos),
                        nameof(branch.Telefono),
                        nameof(branch.Correo),
                        nameof(branch.Calle),
                        nameof(branch.NumeroExterior),
                        nameof(branch.NumeroInterior),
                        nameof(branch.Codigopostal),
                        nameof(branch.ColoniaId),
                        nameof(branch.Ciudad),
                        nameof(branch.Estado),
                        nameof(branch.Matriz),
                        nameof(branch.Activo));

                    context.Database.ExecuteSqlRaw(script);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // Parameters
            if (update)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var parameters = GetParameters();
                    var parameter = new Parameter();

                    var script = MergeGenerator.Build(
                        nameof(context.CAT_Parametro),
                        parameters,
                        nameof(parameter.Id),
                        nameof(parameter.Clave),
                        nameof(parameter.Nombre),
                        nameof(parameter.NombreCorto),
                        nameof(parameter.TipoValor),
                        nameof(parameter.Formula),
                        nameof(parameter.AreaId),
                        nameof(parameter.DepartamentoId),
                        nameof(parameter.UnidadId),
                        nameof(parameter.UnidadSiId),
                        nameof(parameter.FCSI),
                        nameof(parameter.Activo));

                    //context.Database.ExecuteSqlRaw(script);

                    context.BulkInsertOrUpdate(parameters);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // Studies
            if (update)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var studies = GetStudies();
                    var study = new Study();

                    var script = MergeGenerator.Build(
                        nameof(context.CAT_Estudio),
                        studies,
                        nameof(study.Id),
                        nameof(study.Clave),
                        nameof(study.Nombre),
                        nameof(study.NombreCorto),
                        nameof(study.Orden),
                        nameof(study.Titulo),
                        nameof(study.AreaId),
                        nameof(study.DepartamentoId),
                        nameof(study.Visible),
                        nameof(study.DiasResultado),
                        nameof(study.Dias),
                        nameof(study.TiempoResultado),
                        nameof(study.MaquiladorId),
                        nameof(study.MetodoId),
                        nameof(study.TaponId),
                        nameof(study.Cantidad),
                        nameof(study.Prioridad),
                        nameof(study.Urgencia),
                        nameof(study.Activo));

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Estudio)} ON;");
                    context.Database.ExecuteSqlRaw(script);
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Estudio)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // Packs
            if (update)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var packs = GetPacks();
                    var pack = new Packet();

                    var script = MergeGenerator.Build(
                        nameof(context.CAT_Paquete),
                        packs,
                        nameof(pack.Id),
                        nameof(pack.Clave),
                        nameof(pack.Nombre),
                        nameof(pack.NombreLargo),
                        nameof(pack.AreaId),
                        nameof(pack.DepartamentoId),
                        nameof(pack.Visibilidad),
                        nameof(pack.Activo));

                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Paquete)} ON;");
                    context.Database.ExecuteSqlRaw(script);
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {nameof(context.CAT_Paquete)} OFF;");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // StudyIndications
            if (update)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var studyIndications = GetStudyIndications();
                    var si = new IndicationStudy();

                    var script = MergeGenerator.Build(
                        nameof(context.Relacion_Estudio_Indicacion),
                        studyIndications,
                        new string[] { nameof(si.EstudioId), nameof(si.IndicacionId) },
                        nameof(si.Activo));

                    //context.Database.ExecuteSqlRaw(script);
                    context.BulkInsertOrUpdate(studyIndications);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // StudyParameters
            if (update)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var studyParameters = GetStudyParameters();
                    var sp = new ParameterStudy();

                    var script = MergeGenerator.Build(
                        nameof(context.Relacion_Estudio_Parametro),
                        studyParameters,
                        new string[] { nameof(sp.EstudioId), nameof(sp.ParametroId) },
                        nameof(sp.Activo));

                    //context.Database.ExecuteSqlRaw(script);
                    context.BulkInsertOrUpdate(studyParameters);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            // PackStudies
            if (update)
            {
                using var transaction = context.Database.BeginTransaction();

                try
                {
                    var packStudies = GetPackStudies();
                    var ps = new PacketStudy();

                    var script = MergeGenerator.Build(
                        nameof(context.Relacion_Estudio_Paquete),
                        packStudies,
                        new string[] { nameof(ps.PacketId), nameof(ps.EstudioId) },
                        nameof(ps.Activo));

                    //context.Database.ExecuteSqlRaw(script);
                    context.BulkInsertOrUpdate(packStudies);

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
                new Payment(22, "99", "PD", "Por definir"),
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

        /*private static List<ParameterValue> GetParameterValue()
        {
            var path = "wwwroot/seed/CAT_VALORES_REFERENCIA.xlsx";
            var tableData = ReadAsTable(path);

            var maquilas = tableData.AsEnumerable().Select(x => new ParameterValue(
                Convert.ToInt32(x.Field<double>("Id")),
                x.Field<string>("Clave"),
                x.Field<string>("Nombre") ?? ""
                )).ToList();

            return maquilas;
        }*/

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

        private static List<Units> GetUnits()
        {
            var path = "wwwroot/seed/CAT_PARAMETROS.xlsx";
            var tableData = ReadAsTable(path);

            var units = tableData.AsEnumerable().Select(x => new Units(
                    Convert.ToInt32(x.Field<double>("UnidadId")),
                    x.Field<string>("Unidad")
                    )).ToList();

            units = units.Where(x => x.Id > 0).GroupBy(x => x.Id).Select(x => x.First()).ToList();

            return units;
        }

        private static List<Parameter> GetParameters()
        {
            var path = "wwwroot/seed/CAT_PARAMETROS.xlsx";
            var tableData = ReadAsTable(path);

            var areas = GetAreas();

            var units = GetUnits();

            var parameters = tableData.AsEnumerable().Select(x =>
            {
                var area = areas.FirstOrDefault(a => a.Clave == x.Field<string>("Area"));
                var type = GetValueType(x.Field<string>("TipoR"));
                var unit = units.FirstOrDefault(u => u.Clave == x.Field<string>("Unidad"));

                return new Parameter(
                    Guid.Parse(x.Field<string>("Id")),
                    x.Field<string>("Clave"),
                    x.Field<string>("Nombre"),
                    x.Field<string>("Corto"),
                    type,
                    x.Field<string>("Formula"),
                    area?.Id,
                    area?.DepartamentoId,
                    unit?.Id,
                    unit?.Id,
                    x.Field<string>("Fcsi"));
            }).ToList();

            return parameters;
        }

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
                    area?.Id,
                    area?.DepartamentoId,
                    maquila?.Id,
                    method?.Id,
                    tag?.Id);
            }).ToList();

            return studies;
        }

        private static List<Packet> GetPacks()
        {
            var path = "wwwroot/seed/CAT_PAQUETES.xlsx";
            var tableData = ReadAsTable(path);

            var packs = tableData.AsEnumerable().Select(x =>
            {
                return new Packet(
                    Convert.ToInt32(x.Field<double>("Id")),
                    x.Field<string>("Clave"),
                    x.Field<string>("Nombre"),
                    x.Field<string>("Titulo"),
                    AREAS.PAQUETES,
                    DEP.PAQUETES,
                    x.Field<string>("Visible") == "V");
            }).ToList();

            return packs;
        }

        private static List<IndicationStudy> GetStudyIndications()
        {
            var path = "wwwroot/seed/CAT_ESTUDIOS_INDI.xlsx";
            var tableData = ReadAsTable(path);

            var studies = GetStudies();
            var indications = GetIndications();

            var studyIndications = tableData.AsEnumerable().Select(x =>
            {
                var study = studies.FirstOrDefault(s => s.Clave == x.Field<string>("ClaveEstudio"));
                var indication = indications.FirstOrDefault(i => i.Clave == x.Field<string>("ClaveIndicacion"));

                return new IndicationStudy(indication?.Id ?? 0, study?.Id ?? 0);
            }).ToList();

            studyIndications = studyIndications.Where(x => x.IndicacionId > 0 && x.EstudioId > 0).ToList();

            studyIndications = studyIndications.GroupBy(x => new { x.EstudioId, x.IndicacionId }).Select(x => x.First()).ToList();

            return studyIndications;
        }

        private static List<ParameterStudy> GetStudyParameters()
        {
            var path = "wwwroot/seed/CAT_ESTUDIOS_GRUPOS.xlsx";
            var tableData = ReadAsTable(path);

            var studies = GetStudies();
            var parameters = GetParameters();

            var studyParameters = tableData.AsEnumerable()
                .Where(x => !x.Field<string>("Clave").StartsWith("_") && x.Field<string>("ClaveR").StartsWith("_"))
                .Select(x =>
            {
                var study = studies.FirstOrDefault(s => s.Clave == x.Field<string>("Clave"));
                var parameter = parameters.FirstOrDefault(i => i.Clave == x.Field<string>("ClaveR"));

                return new ParameterStudy(parameter?.Id ?? Guid.Empty, study?.Id ?? 0);
            }).ToList();

            studyParameters = studyParameters.Where(x => x.ParametroId != Guid.Empty && x.EstudioId > 0).ToList();

            studyParameters = studyParameters.GroupBy(x => new { x.EstudioId, x.ParametroId }).Select(x => x.First()).ToList();

            return studyParameters;
        }

        private static List<PacketStudy> GetPackStudies()
        {
            var path = "wwwroot/seed/CAT_ESTUDIOS_GRUPOS.xlsx";
            var tableData = ReadAsTable(path);

            var packs = GetPacks();
            var studies = GetStudies();

            var packStudies = tableData.AsEnumerable()
                .Where(x => !x.Field<string>("Clave").StartsWith("_") && !x.Field<string>("ClaveR").StartsWith("_"))
                .Select(x =>
            {
                var pack = packs.FirstOrDefault(i => i.Clave == x.Field<string>("Clave"));
                var study = studies.FirstOrDefault(s => s.Clave == x.Field<string>("ClaveR"));

                return new PacketStudy(pack?.Id ?? 0, study?.Id ?? 0);
            }).ToList();

            packStudies = packStudies.Where(x => x.PacketId > 0 && x.EstudioId > 0).ToList();

            packStudies = packStudies.GroupBy(x => new { x.PacketId, x.EstudioId }).Select(x => x.First()).ToList();

            return packStudies;
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
