using FluentValidation;
using Shared.Validators;
using System;

namespace Service.Catalog.Dtos.Maquilador
{
    public class MaquilaFormDto
    {
        public int Id { set; get; }
        public string Clave { set; get; }
        public string Nombre { set; get; }
        public string Correo { set; get; }
        public string Telefono { set; get; }
        public string PaginaWeb { set; get; }
        public string CodigoPostal { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Calle { get; set; }
        public int ColoniaId { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioId { get; set; }
    }

    public class MaquiladorFormDtoValidator : AbstractValidator<MaquilaFormDto>
    {
        public MaquiladorFormDtoValidator()
        {
            RuleFor(x => x.Clave).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.CodigoPostal).NotEmpty().MinimumLength(5).MaximumLength(5);
            RuleFor(x => x.Correo).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(x => x.PaginaWeb).MaximumLength(100);
            RuleFor(x => x.NumeroExterior).NotEmpty().MaximumLength(10);
            RuleFor(x => x.NumeroInterior).MaximumLength(10);
            RuleFor(x => x.Calle).MaximumLength(100);
            RuleFor(x => x.Telefono).PhoneNumber();
        }
    }
}
