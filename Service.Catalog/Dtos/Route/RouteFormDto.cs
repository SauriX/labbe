using Service.Catalog.Dtos.Promotion;
using System;
using System.Collections.Generic;
using FluentValidation;

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
        public int? HoraDeRecoleccion { get; set; }
        public int DiasDeEntrega { get; set; }
        public int TiempoDeEntrega { get; set; }
        public Decimal FormatoDeTiempoId { get; set; }
        public string EstudioId { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioId { get; set; }
        public IEnumerable<DiasDto> Dias { get; set; }
        public IEnumerable<Route_StudyListDto> Estudio { get; set; }
    }
    public class RouteFormDtoValidator : AbstractValidator<RouteFormDto>
    {
        public RouteFormDtoValidator()
        {
            RuleFor(x => x.Clave).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.SucursalOrigenId).NotEmpty().MaximumLength(50);
            RuleFor(x => x.SucursalDestinoId).NotEmpty().MaximumLength(50);
            RuleFor(x => x.TiempoDeEntrega).NotEmpty();
            RuleFor(x => x.FormatoDeTiempoId).NotEmpty();
            RuleFor(x => x.Comentarios).MaximumLength(500);
        }
    }
}
