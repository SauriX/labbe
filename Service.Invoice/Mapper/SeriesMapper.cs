using Service.Billing.Domain.Series;
using Service.Billing.Dtos.Series;
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
                //Sucursal = x.Sucursal?.Clave,
                Activo = x.Activo,
                TipoSerie = x.TipoSerie == TIPO_FACTURA ? "Factura" : "Recibo"
            });
        }
    }
}
