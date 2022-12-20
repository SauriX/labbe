using ClosedXML.Excel;
using Service.Catalog.Domain.Branch;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Indication;
using Service.Catalog.Domain.Maquila;
using Service.Catalog.Domain.Tapon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using PL = Shared.Dictionary.Catalogs.PriceList;
using DEP = Shared.Dictionary.Catalogs.Department;
using AREAS = Shared.Dictionary.Catalogs.Area;
using BR = Shared.Dictionary.Catalogs.Branch;
using COMP = Shared.Dictionary.Catalogs.Company;
using MED = Shared.Dictionary.Catalogs.Medic;
using OR = Shared.Dictionary.Catalogs.Origin;
using ValueTypes = Shared.Dictionary.Catalogs.TipoValor;
using Service.Catalog.Domain.Parameter;
using Service.Catalog.Domain.Study;
using Service.Catalog.Domain.Packet;
using Shared.Extensions;
using Service.Catalog.Domain.Configuration;
using Service.Catalog.Domain.Company;
using Service.Catalog.Domain.Medics;
using Service.Catalog.Domain.Price;
using Service.Catalog.Domain.Provenance;

namespace Service.Catalog.Context.SeedData
{
    public class SeedData
    {
        // PROCEDENCIA
        public static List<Provenance> GetOrigins()
        {
            var origins = new List<Provenance>
            {
                new Provenance(OR.COMPAÑIA, "COMPAÑIA", "COMPAÑIA"),
                new Provenance(OR.PARTICULAR, "PARTICULAR", "PARTICULAR")
            };

            return origins;
        }

        // CCOMPAÑIA DEFAULT
        public static Company GetDefaultCompany()
        {
            var company = new Company(
                COMP.PARTICULARES,
                "PARTICULARES",
                "PARTICULARES",
                "PARTICULARES",
                OR.PARTICULAR);

            return company;
        }

        // MEDICO DEFAULT
        public static Medics GetDefaultMedic()
        {
            var medic = new Medics(
                MED.A_QUIEN_CORRESPONDA,
                "AQC",
                "A",
                "QUIEN",
                "CORRESPONDA",
                "AQUIENCORRESPONDA");

            return medic;
        }

        // CONFIGURACION
        public static List<Configuration> GetConfiguration()
        {
            var configuration = new List<Configuration>()
            {
                new Configuration(1, "Correo"),
                new Configuration(2, "Remitente"),
                new Configuration(3, "SMTP" ),
                new Configuration(4, "Requiere Contraseña"),
                new Configuration(5, "Contraseña"),
                new Configuration(6, "Nombre Sistema"),
                new Configuration(7, "Logo","logo.png"),
                new Configuration(8, "RFC"),
                new Configuration(9, "Razón Social"),
                new Configuration(10, "CP"),
                new Configuration(11, "Estado"),
                new Configuration(12, "Colonia"),
                new Configuration(13, "Calle"),
                new Configuration(14, "Número"),
                new Configuration(15, "Teléfono"),
                new Configuration(16, "Ciudad")
            };

            return configuration;
        }

        // FORMAS DE PAGO
        public static List<Payment> GetPaymentForms()
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

        // USOS DE CFDI
        public static List<UseOfCFDI​> GetUseOfCFDI​s()
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

        // SUCURSALES
        public static List<Branch> GetBranches()
        {
            var branches = new List<Branch>
            {
                new Branch(BR.MT,           "01", "MT",            "Matriz",                   1, "110000-110999", "644-415-16-92", "laboratorioramos.recepcion@gmail.com",   "Sinaloa",                      "144",  null,   "85000", 123661, "Cajeme",                   "Sonora"),
                new Branch(BR.CMSS,         "02", "CMSS",          "Centro Médico Sur Sonora", 0, "111000-111299", "644-104-22-30", "labramoscmss@gmail.com",                 "Norte",                        null,   "749",  "85040", 123705, "Cajeme",                   "Sonora"),
                new Branch(BR.U200,         "03", "U200",          "Unidad 200",               0, "111300-111599", "644-416-14-07", "labramos200@gmail.com",                  "200",                          null,   null,   "85150", 123819, "Cajeme",                   "Sonora"),
                new Branch(BR.U300,         "04", "U300",          "Unidad 300",               0, "111600-111899", "644-444-66-69", "labramos300@gmail.com",                  "Jalisco",                      null,   "2250", "85080", 123739, "Cajeme",                   "Sonora"),
                new Branch(BR.ALAMEDA,      "05", "ALAMEDA",       "Unidad Alameda",           0, "111900-112199", "644-418-01-44", "labramoscalifornia@gmail.com",           "California",                   "358",  null,   "85219", 125349, "Cajeme",                   "Sonora"),
                new Branch(BR.HACIENDAS,    "06", "HACIENDAS",     "Unidad Las Haciendas",     0, "112200-112499", "644-104-22-30", "labramoshaciendas@gmail.com",            "No Reelección",                "2425", "1",    "85064", 123730, "Cajeme",                   "Sonora"),
                new Branch(BR.REFORMA,      "07", "REFORMA",       "Unidad Reforma",           0, "131600-131899", "662-213-68-66", "labramosreforma@gmail.com",              "Reforma",                      "273",  null,   "83270", 119437, "Hermosillo",               "Sonora"),
                new Branch(BR.MORELOS,      "08", "MORELOS",       "Unidad Morelos",           0, "131300-131599", "662-267-86-35", "lab.ramos.morelos@hotmail.com",          "Blvd. José María Morelos",     "357",  null,   "83144", 119192, "Hermosillo",               "Sonora"),
                new Branch(BR.SOLIDARIDAD,  "09", "SOLIDARIDAD",   "Unidad Solidaridad",       1, "130000-130999", "662-216-41-38", "solidaridad.labramos@gmail.com",         "Solidaridad 2",                null,   null,   "83200", 119328, "Hermosillo",               "Sonora"),
                new Branch(BR.MNORTE,       "10", "MNORTE",        "Unidad Médica Norte",      0, "131000-131299", "662-118-76-67", "labramos.medicanorte@gmail.com",         "Blvd. Solidaridad",            "574",  null,   "83110", 119063, "Hermosillo",               "Sonora"),
                new Branch(BR.CANTABRIA,    "11", "CANTABRIA",     "Unidad Cantabria",         0, "131900-132199", "662-980-04-16", "labramos.cantabria@gmail.com ",          "Blvd. Colosio",                "803",  "14",   "83224", 119360, "Hermosillo",               "Sonora"),
                new Branch(BR.UNIDAD,       "12", "UNIDAD",        "Unidad Guaymas",           1, "150001-150999", "622-221-91-83", "labramos.guaymas@gmail.com",             "Av. Calzada A. García López",  null,   null,   "85440", 126411, "Heroica Guaymas",          "Sonora"),
                new Branch(BR.NAVOJOA,      "13", "NAVOJOA",       "Navojoa Talamante",        1, "120001-120999", "642-421-19-41", "lramos.navojoa@gmail.com",               "Talamante",                    "703",  null,   "85870", 127452, "Navojoa",                  "Sonora"),
                new Branch(BR.NAVOJOA2,     "14", "NAVOJOA2",      "Navojoa Quintana Roo",     0, "121000-121299", "642-120-01-44", "lramos.navojoa2@gmail.com",              "Av. Quintana Roo Oriente",     "106",  null,   "85870", 127452, "Navojoa",                  "Sonora"),
                new Branch(BR.KENNEDY,      "15", "KENNEDY",       "Nogales Kennedy",          1, "140000-140999", "631-690-18-30", "labramos.nogales@gmail.com",             "Av. Kennedy",                  "156",  null,   "84066", 121756, "Nogales",                  "Sonora"),
                new Branch(BR.NOGALES1,     "16", "NOGALES1",      "Nogales Álvaro Obregón",   0, "141000-141299", "631-209-91-39", "labramos.nogales@gmail.com",             "Av. Álvaro Obregón",           "1623", null,   "84055", 121715, "Nogales",                  "Sonora"),
                new Branch(BR.CUMBRES,      "17", "CUMBRES",       "Cumbres, Monterrey",       1, "210000-210999", "818-526-00-22", "labramos.mty@gmail.com",                 "Av. Paseo de los Leones",      "2301", null,   "64610",  87256, "Monterrey",                "Nuevo León"),
                new Branch(BR.SPGG,         "18", "SPGG",          "San Pedro Garza García",   1, "220000-220999", "814-170-07-69", "labramos.cumbres@gmail.com",             "Av. Humberto Lobo",            "555A", null,   "66220",  88801, "San Pedro Garza García",   "Nuevo León" ),
            };

            return branches;
        }

        // LISTA DE PRECIOS DEFAULT
        public static PriceList GetDefaultPriceList()
        {
            var priceList = new PriceList(
                PL.PARTICULARES,
                "PARTICULARES",
                "PARTICULARES",
                true);

            priceList.Compañia = new List<Price_Company>
            {
                new Price_Company(PL.PARTICULARES, COMP.PARTICULARES)
            };

            var branches = GetBranches();
            priceList.Sucursales = branches.Select(x => new Price_Branch(PL.PARTICULARES, x.Id)).ToList();

            return priceList;
        }

        // SUCURSALES CONFIGURACION FOLIO
        public static List<BranchFolioConfig> GetClinicosConfig()
        {
            var config = new List<BranchFolioConfig>()
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

            return config;
        }

        // DEPARTAMENTOS
        public static List<Department> GetDepartments()
        {
            var path = "wwwroot/seed/01_CAT_DEPARTAMENTOS.xlsx";
            var tableData = ReadAsTable(path, "DEPARTAMENTOS");

            var departments = tableData.AsEnumerable().Select(x => new Department(
                Convert.ToInt32(x.Field<double>("Id")),
                x.Field<string>("Clave"),
                x.Field<string>("Nombre")
                )).ToList();

            return departments;
        }

        // AREAS
        public static List<Area> GetAreas()
        {
            var path = "wwwroot/seed/02_CAT_AREAS.xlsx";
            var tableData = ReadAsTable(path, "AREAS");

            var areas = tableData.AsEnumerable().Select(x => new Area(
                Convert.ToInt32(x.Field<double>("Id")),
                Convert.ToInt32(x.Field<double>("DepartamentoId")),
                x.Field<string>("Clave"),
                x.Field<string>("Nombre"),
                Convert.ToInt32(x.Field<double>("Orden"))
                )).ToList();

            return areas;
        }

        // ETIQUETAS
        public static List<Tapon> GetTags()
        {
            var path = "wwwroot/seed/03_CAT_ETIQUETAS.xlsx";
            var tableData = ReadAsTable(path, "ETIQUETAS");

            var tags = tableData.AsEnumerable().Select(x => new Tapon(
                Convert.ToInt32(x.Field<double>("Id")),
                x.Field<string>("Clave"),
                x.Field<string>("Nombre"),
                x.Field<string>("Color")
                )).ToList();

            foreach (var tag in tags)
            {
                tag.Color = string.IsNullOrWhiteSpace(tag.Color) ? null : tag.Color;
            }

            return tags;
        }

        // INDICACIONES
        public static List<Indication> GetIndications()
        {
            var path = "wwwroot/seed/04_CAT_INDICACIONES.xlsx";
            var tableData = ReadAsTable(path, "INDICACIONES");

            var maquilas = tableData.AsEnumerable().Select(x => new Indication(
                Convert.ToInt32(x.Field<double>("Id")),
                x.Field<string>("Clave"),
                x.Field<string>("Nombre")
                )).ToList();

            return maquilas;
        }

        // METODOS
        public static List<Method> GetMethods()
        {
            var path = "wwwroot/seed/05_CAT_METODOS.xlsx";
            var tableData = ReadAsTable(path, "METODOS");

            var methods = tableData.AsEnumerable().Select(x => new Method(
                Convert.ToInt32(x.Field<double>("Id")),
                x.Field<string>("Clave"),
                x.Field<string>("Nombre")
                )).ToList();

            return methods;
        }

        // MAQUILADORES
        public static List<Maquila> GetMaquilas()
        {
            var path = "wwwroot/seed/06_CAT_MAQUILADORES.xlsx";
            var tableData = ReadAsTable(path, "MAQUILADORES");

            var maquilas = tableData.AsEnumerable().Select(x => new Maquila(
                Convert.ToInt32(x.Field<double>("Id")),
                x.Field<string>("Clave"),
                x.Field<string>("Nombre")
                )).ToList();

            return maquilas;
        }

        // UNIDADES
        public static List<Units> GetUnits()
        {
            var path = "wwwroot/seed/07_CAT_PAR_EST_PAQ.xlsx";
            var tableData = ReadAsTable(path, "PARAMETROS");

            var units = tableData.AsEnumerable()
                .Where(x => (int?)(x.Field<object>("UnidadId").NullIfEmpty() as double?) != null)
                .Select(x => new Units(
                    Convert.ToInt32(x.Field<double>("UnidadId")),
                    x.Field<string>("Unidad")
                    )).ToList();

            units = units.Where(x => x.Id > 0).GroupBy(x => x.Id).Select(x => x.First()).ToList();

            return units;
        }

        // PARAMETROS
        public static List<Parameter> GetParameters()
        {
            var path = "wwwroot/seed/07_CAT_PAR_EST_PAQ.xlsx";
            var tableData = ReadAsTable(path, "PARAMETROS");

            var parameters = tableData.AsEnumerable().Select(x =>
            {
                var type = GetValueType(x.Field<string>("TipoR"));

                return new Parameter(
                    Guid.Parse(x.Field<string>("Id")),
                    x.Field<string>("Clave"),
                    x.Field<string>("Nombre"),
                    x.Field<string>("Corto"),
                    type,
                    x.Field<string>("Formula"),
                    (int?)(x.Field<object>("UnidadId").NullIfEmpty() as double?),
                    (int?)(x.Field<object>("UnidadId").NullIfEmpty() as double?),
                    x.Field<string>("FcsiFormato"));
            }).ToList();

            return parameters;
        }

        // ESTUDIOS
        public static List<Study> GetStudies()
        {
            var path = "wwwroot/seed/07_CAT_PAR_EST_PAQ.xlsx";
            var tableData = ReadAsTable(path, "ESTUDIOS");

            //var tagsR = ReadAsTable("wwwroot/seed/08_REL_EST_ETQ.xlsx", "ESTUDIO-ETIQUETA").AsEnumerable();

            var studies = tableData.AsEnumerable().Select(x =>
            {
                return new Study(
                    Convert.ToInt32(x.Field<double>("Id")),
                    x.Field<string>("Clave"),
                    x.Field<string>("Nombre"),
                    Convert.ToInt32(x.Field<double>("Orden")),
                    x.Field<string>("Titulo"),
                    x.Field<string>("Corto"),
                    x.Field<string>("Visible") == "V",
                    Convert.ToInt32(x.Field<double>("Dias")),
                    (int?)(x.Field<object>("AreaId").NullIfEmpty() as double?),
                    (int?)(x.Field<object>("DepartamentoId").NullIfEmpty() as double?),
                    (int?)(x.Field<object>("MaquiladorId").NullIfEmpty() as double?),
                    (int?)(x.Field<object>("MetodoId").NullIfEmpty() as double?));
            }).ToList();

            return studies;
        }

        // PAQUETES
        public static List<Packet> GetPacks()
        {
            var path = "wwwroot/seed/07_CAT_PAR_EST_PAQ.xlsx";
            var tableData = ReadAsTable(path, "PAQUETES");

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

        // PARAMETROS VALORES
        public static List<ParameterValue> GetParameterValues()
        {
            var path = "wwwroot/seed/09_REL_PAR_VAL.xlsx";
            var tableData = ReadAsTable(path, "PARAMETRO-VALOR");

            var parameterValues = tableData.AsEnumerable().Select((x, i) =>
            {
                var id = Guid.Parse(x.Field<string>("Id"));
                var parameterId = Guid.Parse(x.Field<string>("ParametroId"));
                var type = GetValueType(x.Field<string>("Tipo"));
                var order = Convert.ToInt32(x.Field<double>("Orden"));
                var gender = x.Field<string>("Sexo");
                var startAge = x.Field<string>("EdadInicial").ToLower();
                var endAge = x.Field<string>("EdadFinal").ToLower();
                var value1 = x.Field<string>("Valor1");
                var value2 = x.Field<string>("Valor2");
                var value3 = x.Field<string>("Valor3");
                var value4 = x.Field<string>("Valor4");
                var value5 = x.Field<string>("Valor5");
                var value6 = x.Field<string>("Valor6");
                var label = x.Field<string>("Etiqueta");
                var multiple = x.Field<string>("Multiple");

                var valueNum1 = 0m;
                var valueNum2 = 0m;

                var initAgeType = 3;
                var initAge = 0;
                var finalAge = 0;

                if (type.In(ValueTypes.Numerico, ValueTypes.NumericoPorSexo, ValueTypes.NumericoPorEdad, ValueTypes.NumericoPorEdadSexo))
                {
                    value1 = string.IsNullOrWhiteSpace(value1) || value1 == "\\N" ? "0" : value1.Split(" ")[0];
                    value2 = string.IsNullOrWhiteSpace(value2) || value2 == "\\N" ? "0" : value2.Split(" ")[0];

                    var initOk = decimal.TryParse(value1, out valueNum1);
                    var endOk = decimal.TryParse(value2, out valueNum2);

                    if (!initOk || !endOk)
                    {
                        return null;
                    }
                }

                if (type.In(ValueTypes.NumericoPorEdad, ValueTypes.NumericoPorEdadSexo))
                {
                    initAgeType = startAge.Contains("a") ? 3 : startAge.Contains("m") ? 2 : startAge.Contains("d") ? 1 : 3;

                    var intiAgeStr = startAge.Split(".")[0].Replace(new string[] { "años", "a", "meses", "mes", "m", "d", " " }, "");
                    var endAgeStr = endAge.Split(".")[0].Replace(new string[] { "años", "a", "meses", "mes", "m", "d", " " }, "");

                    var initOk = int.TryParse(intiAgeStr, out initAge);
                    var endOk = int.TryParse(endAgeStr, out finalAge);

                    if (!initOk || !endOk)
                    {
                        return null;
                    }
                }

                if (type == ValueTypes.Numerico)
                {
                    return new ParameterValue
                    {
                        Id = id,
                        ParametroId = parameterId,
                        Nombre = ValueTypes.Numerico,
                        ValorInicial = valueNum1,
                        ValorFinal = valueNum2,
                        Orden = order,
                    };
                }
                else if (type == ValueTypes.NumericoPorSexo)
                {
                    return new ParameterValue
                    {
                        Id = id,
                        ParametroId = parameterId,
                        Nombre = gender == "M" ? "hombre" : "mujer",
                        HombreValorInicial = gender == "M" ? valueNum1 : 0m,
                        HombreValorFinal = gender == "M" ? valueNum2 : 0m,
                        MujerValorInicial = gender == "F" ? valueNum1 : 0m,
                        MujerValorFinal = gender == "F" ? valueNum2 : 0m,
                        Orden = order,
                    };
                }
                else if (type == ValueTypes.NumericoPorEdad)
                {
                    return new ParameterValue
                    {
                        Id = id,
                        ParametroId = parameterId,
                        Nombre = ValueTypes.NumericoPorEdad,
                        RangoEdadInicial = initAge,
                        RangoEdadFinal = finalAge,
                        ValorInicialNumerico = valueNum1,
                        ValorFinalNumerico = valueNum2,
                        Orden = order,
                    };
                }
                else if (type == ValueTypes.NumericoPorEdadSexo)
                {
                    return new ParameterValue
                    {
                        Id = id,
                        ParametroId = parameterId,
                        Nombre = gender == "M" ? "hombre" : "mujer",
                        RangoEdadInicial = initAge,
                        RangoEdadFinal = finalAge,
                        ValorInicialNumerico = valueNum1,
                        ValorFinalNumerico = valueNum2,
                        Orden = order,
                    };
                }
                else if (type == ValueTypes.OpcionMultiple)
                {
                    return new ParameterValue
                    {
                        Id = id,
                        ParametroId = parameterId,
                        Nombre = ValueTypes.OpcionMultiple,
                        Opcion = multiple,
                        Orden = order,
                    };
                }
                else if (type == ValueTypes.Texto)
                {
                    return new ParameterValue
                    {
                        Id = id,
                        ParametroId = parameterId,
                        Nombre = ValueTypes.Texto,
                        DescripcionTexto = multiple,
                        Orden = order,
                    };
                }
                else if (type == ValueTypes.Parrafo)
                {
                    return new ParameterValue
                    {
                        Id = id,
                        ParametroId = parameterId,
                        Nombre = ValueTypes.Parrafo,
                        DescripcionParrafo = multiple,
                        Orden = order,
                    };
                }
                else if (type == ValueTypes.Etiqueta)
                {
                    return new ParameterValue
                    {
                        Id = id,
                        ParametroId = parameterId,
                        Nombre = ValueTypes.Etiqueta,
                        Opcion = label,
                        Orden = order,
                    };
                }
                else if (type == ValueTypes.Observacion)
                {
                    return new ParameterValue
                    {
                        Id = id,
                        ParametroId = parameterId,
                        Nombre = ValueTypes.Observacion,
                        DescripcionTexto = multiple,
                        Orden = order,
                    };
                }
                else if (type == ValueTypes.NumericoCon1Columna)
                {
                    return new ParameterValue
                    {
                        Id = id,
                        ParametroId = parameterId,
                        Nombre = ValueTypes.NumericoCon1Columna,
                        PrimeraColumna = value1,
                        Orden = order,
                    };
                }
                else if (type == ValueTypes.NumericoCon2Columna)
                {
                    return new ParameterValue
                    {
                        Id = id,
                        ParametroId = parameterId,
                        Nombre = ValueTypes.NumericoCon2Columna,
                        PrimeraColumna = value1,
                        SegundaColumna = value2,
                        Orden = order,
                    };
                }
                else if (type == ValueTypes.NumericoCon3Columna)
                {
                    return new ParameterValue
                    {
                        Id = id,
                        ParametroId = parameterId,
                        Nombre = ValueTypes.NumericoCon3Columna,
                        PrimeraColumna = value1,
                        SegundaColumna = value2,
                        TerceraColumna = value3,
                        Orden = order,
                    };
                }
                else if (type == ValueTypes.NumericoCon4Columna)
                {
                    return new ParameterValue
                    {
                        Id = id,
                        ParametroId = parameterId,
                        Nombre = ValueTypes.NumericoCon4Columna,
                        PrimeraColumna = value1,
                        SegundaColumna = value2,
                        TerceraColumna = value3,
                        CuartaColumna = value4,
                        Orden = order,
                    };
                }

                return null;
            }).ToList();

            return parameterValues.Where(x => x != null).ToList();
        }

        // ESTUDIOS INDICACIONES
        public static List<IndicationStudy> GetStudyIndications()
        {
            var path = "wwwroot/seed/10_REL_EST_IND.xlsx";
            var tableData = ReadAsTable(path, "ESTUDIO-INDICACION");

            var studyIndications = tableData.AsEnumerable().Select(x =>
            {
                return new IndicationStudy(
                    Convert.ToInt32(x.Field<double>("IndicacionId")),
                    Convert.ToInt32(x.Field<double>("EstudioId")),
                    Convert.ToInt32(x.Field<double>("Orden")));
            }).ToList();

            return studyIndications;
        }

        // ESTUDIOS PARAMETROS
        public static List<ParameterStudy> GetStudyParameters()
        {
            var path = "wwwroot/seed/11_REL_PAR_EST_PAQ.xlsx";
            var tableData = ReadAsTable(path, "ESTUDIO-PARAMETRO");

            var studyParameters = tableData.AsEnumerable()
                .Select(x =>
                {
                    return new ParameterStudy(
                        Convert.ToInt32(x.Field<double>("Id")),
                        Guid.Parse(x.Field<string>("ParametroId")),
                        Convert.ToInt32(x.Field<double>("EstudioId")),
                        Convert.ToInt32(x.Field<double>("Orden")));
                }).ToList();

            return studyParameters;
        }

        // PAQUETES ESTUDIOS
        public static List<PacketStudy> GetPackStudies()
        {
            var path = "wwwroot/seed/11_REL_PAR_EST_PAQ.xlsx";
            var tableData = ReadAsTable(path, "PAQUETE-ESTUDIO");

            var packStudies = tableData.AsEnumerable()
                .Select(x =>
                {
                    return new PacketStudy(
                        Convert.ToInt32(x.Field<double>("PaqueteId")),
                        Convert.ToInt32(x.Field<double>("EstudioId")),
                        Convert.ToInt32(x.Field<double>("Orden")));
                }).ToList();

            return packStudies;
        }

        // PRECIOS ESTUDIOS
        public static List<PriceList_Study> GetStudyPrices()
        {
            var path = "wwwroot/seed/12_CAT_PRECIOS.xlsx";
            var tableData = ReadAsTable(path, "PRECIO-ESTUDIO");

            var studyPrices = tableData.AsEnumerable()
                .Select(x => new PriceList_Study(
                    PL.PARTICULARES,
                    Convert.ToInt32(x.Field<double>("EstudioId")),
                    Convert.ToDecimal(x.Field<double>("Total"))
                    )).ToList();

            return studyPrices;
        }

        public static DataTable ReadAsTable(string filePath, string worksheet)
        {
            using var wb = new XLWorkbook(filePath, XLEventTracking.Disabled);
            var ws = wb.Worksheet(worksheet);
            DataTable dataTable = ws.RangeUsed().AsTable().AsNativeDataTable();

            return dataTable;
        }

        public static string GetValueType(string type)
        {
            return type switch
            {
                "N" => "1",
                "NS" => "2",
                "NR" => "3",
                "NSR" => "4",
                "M" => "5",
                "NT" => "6",
                "NT2" => "11",
                "NT3" => "12",
                "NT4" => "13",
                "T" => "7",
                "P" => "8",
                "L" => "9",
                "O" => "10",
                _ => "0",
            };
        }
    }
}
