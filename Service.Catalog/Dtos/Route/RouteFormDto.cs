using FluentValidation;
using Service.Catalog.Dtos.Common;
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
        public Guid? OrigenId { get; set; }
        public Guid? DestinoId { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public int? MaquiladorId { get; set; }
        public int? PaqueteriaId { get; set; }
        public string Comentarios { get; set; }
        public DateTime HoraDeRecoleccion { get; set; }
        public int TiempoDeEntrega { get; set; }
        public byte TipoTiempo { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioId { get; set; }
        public IEnumerable<DayDto> Dias { get; set; }
        public IEnumerable<Route_StudyListDto> Estudio { get; set; }
    }
    public class RouteFormDtoValidator : AbstractValidator<RouteFormDto>
    {
        public RouteFormDtoValidator()
        {
            RuleFor(x => x.Clave).MaximumLength(100);
            RuleFor(x => x.Nombre).MaximumLength(100);
            RuleFor(x => x.Comentarios).MaximumLength(500);
        }
    }
}
