using Service.Report.Domain.Indicators;
using Service.Report.Domain.MedicalRecord;
using Service.Report.Dtos.Indicators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Service.Report.Mapper
{
    public static class IndicatorsMapper
    {
        public static List<Dictionary<string, object>> ToTableIndicatorsStatsDto(this IEnumerable<IndicatorsStatsDto> model)
        {
            if (model == null) return null;

            string[] rows = { "PACIENTES", "INGRESOS", "COSTO REACTIVO", "COSTO DE TOMA", "COSTO FIJO", "UTILIDAD DE OPERACIÓN" };

            var data = new List<Dictionary<string, object>>();

            foreach (var item in rows)
            {
                var itemData = new Dictionary<string, object>
                {
                    ["NOMBRE"] = item
                };

                foreach (var branch in model)
                {
                    switch (item)
                    {
                        case "PACIENTES":
                            itemData.Add(branch.Sucursal, branch.Pacientes);
                            continue;
                        case "INGRESOS":
                            itemData.Add(branch.Sucursal, branch.Ingresos);
                            continue;
                        case "COSTO REACTIVO":
                            itemData.Add(branch.Sucursal, branch.CostoReactivo);
                            continue;
                        case "COSTO DE TOMA":
                            itemData.Add(branch.Sucursal, branch.CostoTomaCalculado);
                            continue;
                        case "COSTO FIJO":
                            itemData.Add(branch.Sucursal, branch.CostoFijo);
                            continue;
                        case "UTILIDAD DE OPERACIÓN":
                            itemData.Add(branch.Sucursal, branch.UtilidadOperacion);
                            continue;
                    }
                }

                data.Add(itemData);
            }

            return data;
        }

        public static IEnumerable<IndicatorsStatsDto> ToIndicatorsStatsDto(this IEnumerable<RequestInfo> model)
        {
            if (model == null) return null;

            var results = (from c in model
                           group c by new { c.SucursalId, c.Sucursal } into grupo
                           select grupo).Select(grupo =>
                           {
                               return new IndicatorsStatsDto
                               {
                                   Id = Guid.NewGuid(),
                                   Pacientes = grupo.Count(),
                                   SucursalId = grupo.Key.SucursalId,
                                   Sucursal = grupo.Key.Sucursal,
                                   Ingresos = grupo.Sum(x => x.TotalEstudios),
                                   Expedientes = grupo.GroupBy(x => x.Expediente).Count(),
                               };
                           }
                           );

            return results;
        }

        public static IEnumerable<IndicatorsStatsDto> ToTotals(this IEnumerable<RequestInfo> model)
        {
            if (model == null) return null;

            var results = (from c in model
                           group c by new { c.Ciudad } into grupo
                           select grupo).Select(grupo =>
                           {
                               return new IndicatorsStatsDto
                               {
                                   Id = Guid.NewGuid(),
                                   Pacientes = grupo.Count(),
                                   Ciudad = "Total" + grupo.Key.Ciudad,
                                   Ingresos = grupo.Sum(x => x.TotalEstudios),
                                   Expedientes = grupo.GroupBy(x => x.Expediente).Count(),
                               };
                           }
                           );

            return results;
        }

        public static List<Dictionary<string, object>> ToTableTotals(this List<IndicatorsStatsDto> model)
        {
            if (model == null) return null;

            string[] rows = { "PACIENTES", "INGRESOS", "COSTO REACTIVO", "COSTO DE TOMA", "COSTO FIJO", "UTILIDAD DE OPERACIÓN" };

            var data = new List<Dictionary<string, object>>();

            foreach (var item in rows)
            {
                var itemData = new Dictionary<string, object>
                {
                    ["NOMBRE"] = item
                };

                foreach (var branch in model)
                {
                    switch (item)
                    {
                        case "PACIENTES":
                            itemData.Add(branch.Ciudad, branch.Pacientes);
                            continue;
                        case "INGRESOS":
                            itemData.Add(branch.Ciudad, branch.Ingresos);
                            continue;
                        case "COSTO REACTIVO":
                            itemData.Add(branch.Ciudad, branch.CostoReactivo);
                            continue;
                        case "COSTO DE TOMA":
                            itemData.Add(branch.Ciudad, branch.CostoTomaCalculado);
                            continue;
                        case "COSTO FIJO":
                            itemData.Add(branch.Ciudad, branch.CostoFijo);
                            continue;
                        case "UTILIDAD DE OPERACIÓN":
                            itemData.Add(branch.Ciudad, branch.UtilidadOperacion);
                            continue;
                    }
                }

                data.Add(itemData);
            }

            return data;
        }


        public static IEnumerable<SamplesCostsDto> ToSamplesCostsDto(this IEnumerable<SamplesCosts> model)
        {
            if (model == null) return null;

            return model.Select(sample => new SamplesCostsDto
            {
                Id = sample.Id,
                CostoToma = sample.CostoToma,
                SucursalId = sample.SucursalId,
                Sucursal = sample.Sucursal,
                FechaAlta = sample.FechaAlta,
                Aplica = sample.FechaAlta.ToString("MMMM yy", new CultureInfo("ES")),
                Ciudad = sample.Ciudad,
            }).ToList();
        } 

        public static List<Dictionary<string, object>> ToTableServiceCostDto(this List<ServicesCost> model)
        {
            if (model == null) return null;

            List<string> servicesName = new();
            var data = new List<Dictionary<string, object>>();

            foreach (var service in model)
            {
                if (!servicesName.Contains(service.Nombre))
                {
                    servicesName.Add(service.Nombre);
                }
            }

            servicesName.Add("");
            servicesName.Add("Total mesual");
            servicesName.Add("Total semanal");
            servicesName.Add("Total diario");

            foreach (var item in servicesName)
            {
                var itemData = new Dictionary<string, object>
                {
                    ["NOMBRE"] = item
                };

                foreach (var branch in model)
                {
                    if (item == branch.Nombre)
                    {
                        itemData.Add(branch.Sucursal, branch.CostoFijo);
                    }
                }

                data.Add(itemData);
            }

            return data;
        }

        public static IEnumerable<ServicesCostDto> ToServiceCostDto(this IEnumerable<ServicesCost> model)
        {
            if (model == null) return null;

            var results = (from c in model
                           group c by new { c.Sucursal, c.Nombre } into grupo
                           select grupo).Select(grupo =>
                           {
                               return new ServicesCostDto
                               {
                                   Nombre = grupo.Key.Nombre,
                                   Sucursal = grupo.Key.Sucursal,
                                   CostoFijo = grupo.Sum(x => x.CostoFijo),
                               };
                           });

            return results;
        }
        
        public static InvoiceServicesDto ToServiceCostGroupDto(this IEnumerable<ServicesCost> model)
        {
            if (model == null) return null;

            var grupos = model.GroupBy(x => new {x.Id, x.CostoFijo} ).Select(y => y.ToList()).ToList();

            var services = grupos.Select(x => new ServicesCostDto
            {
                Id = x.First().Id,
                Sucursal = string.Join(", ", x.Select(x => x.Sucursal)),
                Nombre = x.First().Nombre,
                CostoFijo = x.First().CostoFijo,
                CostosFijos = x.Sum(x => x.CostoFijo),
                FechaAlta = x.First().FechaAlta
            }).ToList();

            var totals = new InvoiceServicesDto
            {
                Servicios = services,
                TotalMensual = services.Sum(x => x.CostosFijos),
            };

            return totals;
        }

        public static Indicators ToModelCreate(this IndicatorsStatsDto dto)
        {
            if (dto == null) return null;

            return new Indicators
            {
                Id = Guid.NewGuid(),
                CostoReactivo = dto.CostoReactivo,
                SucursalId = dto.SucursalId,
                Fecha = dto.FechaAlta
            };
        }

        public static Indicators ToModelUpdate(this IndicatorsStatsDto dto, Indicators model)
        {
            if (dto == null) return null;

            return new Indicators
            {
                Id = model.Id,
                CostoReactivo = dto.CostoReactivo,
                SucursalId = model.SucursalId,
                Fecha = dto.FechaAlta
            };
        }

        public static SamplesCosts ToSampleUpdate(this SamplesCostsDto dto, SamplesCosts model)
        {
            if (dto == null) return null;

            return new SamplesCosts
            {
                Id = model.Id,
                CostoToma = dto.CostoToma,
                SucursalId = model.SucursalId,
                Sucursal = model.Sucursal,
                FechaAlta = model.FechaAlta, 
                Ciudad = model.Ciudad,
                FechaModificacion = DateTime.Now
            };
        }
    }
}
