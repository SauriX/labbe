using System;
using System.Collections.Generic;

namespace Service.Catalog.Dto.Series
{
    public class SeriesFilterDto
    {
        public DateTime Año { get; set; }
        public List<byte> TipoSeries { get; set; }
        public List<string> Ciudad { get; set; }
        public List<Guid> Sucursales { get; set; }
        public string Buscar { get; set; }
    }

    public class SeriesNewDto
    {
        public string EmisorId => "698E416E-E5CB-4E7A-90ED-74448D408F20";
        public Guid SucursalId { get; set; }
        public Guid UsuarioId { get; set; }
        public byte TipoSerie { get; set; }
    }
}
