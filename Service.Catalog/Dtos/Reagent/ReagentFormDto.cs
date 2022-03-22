using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Dtos.Reagent
{
    public class ReagentFormDto
    {
        public int ReactivoId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string ClaveSistema { get; set; }
        public string NombreSistema { get; set; }
        public bool Activo { get; set; }
        public string UsuarioId { get; set; }
    }

    public class ReactivoFormDtoValidator : AbstractValidator<ReagentFormDto>
    {
        public ReactivoFormDtoValidator()
        {
            RuleFor(x => x.ReactivoId).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Clave).NotEmpty().MaximumLength(15);
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(50);
            RuleFor(x => x.ClaveSistema).MaximumLength(15);
            RuleFor(x => x.NombreSistema).MaximumLength(50);
        }
    }
}
