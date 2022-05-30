using FluentValidation;
using Service.Catalog.Dtos.Catalog;
using Service.Catalog.Dtos.Study;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Indication
{
    public class IndicationFormDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioId { get; set; }

        public IEnumerable<IndicationStudyDto> Estudios { get; set; }
    }

    public class ReactivoFormDtoValidator : AbstractValidator<IndicationFormDto>
    {
        public ReactivoFormDtoValidator()
        {
            RuleFor(x => x.Clave).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Descripcion).MaximumLength(500);
        }
    }
}
