using FluentValidation;
using System;

namespace Service.Catalog.Dtos.Configuration
{
    public class ConfigurationFiscalDto
    {
        public string Rfc { get; set; }
        public string RazonSocial { get; set; }
        public string CodigoPostal { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Telefono { get; set; }
        public Guid UsuarioId { get; set; }
    }

    public class ConfigurationFiscalDtoValidator : AbstractValidator<ConfigurationFiscalDto>
    {
        public ConfigurationFiscalDtoValidator()
        {
            RuleFor(x => x.Rfc).NotEmpty().MaximumLength(4000);
            RuleFor(x => x.RazonSocial).NotEmpty().MaximumLength(4000);
            RuleFor(x => x.CodigoPostal).NotEmpty().MaximumLength(4000);
            RuleFor(x => x.Estado).NotEmpty().MaximumLength(4000);
            RuleFor(x => x.Ciudad).NotEmpty().MaximumLength(4000);
            RuleFor(x => x.Colonia).NotEmpty().MaximumLength(4000);
            RuleFor(x => x.Calle).NotEmpty().MaximumLength(4000);
            RuleFor(x => x.Numero).NotEmpty().MaximumLength(4000);
            RuleFor(x => x.Telefono).MaximumLength(4000);
        }
    }
}
