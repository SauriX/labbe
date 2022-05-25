using FluentValidation;
using System;

namespace Service.Catalog.Dtos.Loyalty
{
    public class LoyaltyFormDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public bool TipoDescuento { get; set; }
        public decimal CantidadDescuento { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
        public long UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public long? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }

    public class LoyaltyFormDtoValidator : AbstractValidator<LoyaltyFormDto>
    {
        public LoyaltyFormDtoValidator()
        {
            RuleFor(x => x.Clave).NotEmpty().MaximumLength(15);
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(50);
            RuleFor(x => x.TipoDescuento).NotEmpty();
            RuleFor(x => x.CantidadDescuento).NotEmpty();
            RuleFor(x => x.FechaInicial).NotEmpty();
            RuleFor(x => x.FechaFinal).NotEmpty();
            RuleFor(x => x.Activo).NotEmpty();
        }
    }
}
