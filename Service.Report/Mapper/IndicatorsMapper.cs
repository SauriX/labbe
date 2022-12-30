using Service.Report.Domain.Indicators;
using Service.Report.Domain.MedicalRecord;
using Service.Report.Dtos.Indicators;
using System;
using System.Collections.Generic;
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

            foreach(var item in rows)
            {
                var itemData = new Dictionary<string, object>
                {
                    ["Nombre"] = item
                };

                foreach(var branch in model)
                {
                    switch (item)
                    {
                        case "PACIENTES":
                            itemData.Add(branch.Sucursal, branch.Pacientes);
                            break;
                        case "INGRESOS":
                            itemData.Add(branch.Sucursal, branch.Ingresos);
                            break;
                        case "COSTO REACTIVO":
                            itemData.Add(branch.Sucursal, branch.CostoReactivo);
                            break;
                        case "COSTO DE TOMA":
                            itemData.Add(branch.Sucursal, branch.CostoTomaCalculado);
                            break;
                        case "COSTO FIJO":
                            itemData.Add(branch.Sucursal, branch.CostoFijo);
                            break;
                        case "UTILIDAD DE OPERACION":
                            itemData.Add(branch.Sucursal, branch.UtilidadOperacion);
                            break;
                    }
                    itemData.Add(branch.Sucursal, branch.Pacientes);
                }

                data.Add(itemData);
            }

            return data;
        }

        public static List<Dictionary<string, object>> ToIndicatorsStatsDto(this IEnumerable<RequestInfo> model, decimal costoFijo, decimal costoReactivo)
        {
            if (model == null) return null;

            var results = (from c in model
                           group c by new { c.SucursalId, c.Sucursal, c.Id} into grupo
                           select grupo).Select(grupo =>
                           {
                               return new IndicatorsStatsDto
                               {
                                   Id = Guid.NewGuid(),
                                   Pacientes = grupo.Count(),
                                   Ingresos = grupo.Sum(x => x.TotalEstudios),
                                   CostoReactivo = costoReactivo,
                                   CostoTomaCalculado = grupo.Sum(x => x.TotalEstudios) * 8.5m,
                                   CostoFijo = costoFijo
                               };
                           }
                           );

            return results.ToTableIndicatorsStatsDto();
        }

        public static List<ServicesCostDto> ServicesCostGeneric(this IEnumerable<ServicesCost> model)
        {
            if (model == null) return null;

            return model.Select(service => new ServicesCostDto
            {
                Id = service.Id,
                Clave = service.Clave,
                Nombre = service.Nombre,
                Sucursal = service.Sucursal,
                CostoFijo = service.CostoFijo,
                FechaAlta = service.FechaAlta,
            }).ToList();
        }

        public static ServicesDto ToServiceCostDto(this IEnumerable<ServicesCost> model)
        {
            if (model == null) return null;

            var results = ServicesCostGeneric(model);

            var costoFijo = results.Select(x => x.CostoFijo).FirstOrDefault();

            var totals = new ServicesCostTimeDto
            {
                TotalMensual = costoFijo,
                TotalSemanal = costoFijo / 6,
                TotalDiario = costoFijo / 24
            };

            var data = new ServicesDto
            {
                CostoServicios = results,
                CostoTemporal = totals
            };

            return data;
        }

        public static List<Indicators> ToModel(this List<IndicatorsStatsDto> dto)
        {
            if (dto == null) return null;

            return dto.Select(x => new Indicators
            {
                Id = Guid.NewGuid(),
                Pacientes = x.Pacientes,
                Ingresos = x.Ingresos,
                CostoTomaCalculado = x.CostoTomaCalculado,
                CostoReactivo = x.CostoReactivo,
                CostoFijo = x.CostoFijo,
                SucursalId = Guid.Parse(x.Sucursal),
                UtilidadOperacion = x.UtilidadOperacion
            }).ToList();
        }
    }
}
