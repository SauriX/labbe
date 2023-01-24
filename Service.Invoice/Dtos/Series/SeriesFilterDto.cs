using System;
using System.Collections.Generic;

namespace Service.Billing.Dto.Series
{
    public class SeriesFilterDto
    {
        public DateTime Año { get; set; }
        public List<byte> TipoSeries { get; set; }
        public List<string> Ciudad { get; set; }
        public List<Guid> Sucursales { get; set; }
        public string Buscar { get; set; }
    }
}
