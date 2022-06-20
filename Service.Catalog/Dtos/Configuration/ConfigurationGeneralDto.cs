using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Service.Catalog.Dtos.Configuration
{
    public class ConfigurationGeneralDto
    {
        public string NombreSistema { get; set; }
        public string LogoRuta { get; set; }
        public IFormFile Logo { get; set; }
    }

    public class ConfigurationGeneralDtoValidator : AbstractValidator<ConfigurationGeneralDto>
    {
        public ConfigurationGeneralDtoValidator()
        {
            RuleFor(x => x.NombreSistema).NotEmpty().MaximumLength(4000);
        }
    }
}
