using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Dtos.Reagent
{
    public class ReagentFormDto
    {
        public string Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string ClaveSistema { get; set; }
        public string NombreSistema { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioId { get; set; }
    }

    public class ReagentFormDtoValidator : AbstractValidator<ReagentFormDto>
    {
        public ReagentFormDtoValidator()
        {
            RuleFor(x => x.Clave).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.ClaveSistema).MaximumLength(100);
            RuleFor(x => x.NombreSistema).MaximumLength(100);
        }
    }
}
