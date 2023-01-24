using Service.Billing.Domain.Series;
using Service.Billing.Dto.Series;
using Service.Billing.Dtos.Series;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Service.Billing.Mapper
{
    public static class SeriesMapper
    {
        private const byte TIPO_FACTURA = 1;

        public static IEnumerable<SeriesListDto> ToSeriesListDto(this List<Series> model)
        {
            if (model == null) return null;

            return model.Select(x => new SeriesListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Descripcion = x.Descripcion,
                Sucursal = x.Sucursal,
                Año = x.FechaCreo.Year.ToString("yyyy"),
                CFDI = x.CFDI,
                Activo = x.Activo,
                TipoSerie = x.TipoSerie == TIPO_FACTURA ? "Factura" : "Recibo"
            });
        }

        public static IEnumerable<TicketListDto> ToTicketListDto(this List<Series> model)
        {
            if (model == null) return null;

            return model.Select(x => new TicketListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Fecha = x.FechaCreo.ToString("dddd/MM/yyyy"),
                TipoSerie = x.TipoSerie == TIPO_FACTURA ? "Factura" : "Recibo"
            });
        }

        public static Series ToModel(this SeriesDto dto)
        {
            if (dto == null) return null;

            return new Series
            {
                Activo = dto.Factura.Activo,
                CFDI = dto.Factura.CFDI,
                Descripcion = dto.Factura.Observaciones,
                Clave = dto.Factura.Clave,
                FechaCreo = DateTime.Now,
                TipoSerie = dto.Factura.TipoSerie,
            };
        }
    }
}
