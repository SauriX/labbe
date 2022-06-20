using FluentValidation;

namespace Service.Catalog.Dtos.Configuration
{
    public class ConfigurationEmailDto
    {
        public string Correo { get; set; }
        public string Remitente { get; set; }
        public string Smtp { get; set; }
        public bool RequiereContraseña { get; set; }
        public string Contraseña { get; set; }
    }

    public class ConfigurationEmailDtoValidator : AbstractValidator<ConfigurationEmailDto>
    {
        public ConfigurationEmailDtoValidator()
        {
            RuleFor(x => x.Correo).NotEmpty().MaximumLength(4000);
            RuleFor(x => x.Remitente).NotEmpty().MaximumLength(4000);
            RuleFor(x => x.Smtp).NotEmpty().MaximumLength(4000);
            RuleFor(x => x.Contraseña).MaximumLength(4000);
        }
    }
}
