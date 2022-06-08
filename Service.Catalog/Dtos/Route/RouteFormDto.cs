using Service.Catalog.Dtos.Promotion;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Route
{
    public class RouteFormDto
    {
        public string Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string SucursalOrigenId { get; set; }
        public string SucursalDestinoId { get; set; }
        public string PaqueteriaId { get; set; }
        public string Comentarios { get; set; }
        public int DiasDeEntrega { get; set; }
        public int TiempoDeEntrega { get; set; }
        public int FormatoDeTiempoId { get; set; }
        public string EstudioId { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioId { get; set; }
        public IEnumerable<DiasDto> Dias { get; set; }
        public IEnumerable<Route_StudyListDto> Estudio { get; set; }
    }
}
