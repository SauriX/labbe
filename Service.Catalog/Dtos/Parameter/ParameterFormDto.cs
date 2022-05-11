using FluentValidation;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Parameter;
using Service.Catalog.Dtos.Catalog;
using Shared.Validators;
using Service.Catalog.Dtos.Study;
using System;
using System.Collections.Generic;
using Service.Catalog.Dtos.Parameter;
using Service.Catalog.Dtos.Reagent;

namespace Service.Catalog.Dtos.Parameters
{
    public class ParameterFormDto
    {
        public string Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public decimal Unidades { get; set; }
        public string TipoValor { get; set; }
        public string Formula { get; set; }
        public string Formato { get; set; }
        public decimal ValorInicial { get; set; }
        public int DepartamentoId { get; set; }
        public int AreaId { get; set; }
        public int FormatoImpresionId { get; set; }
        public string ReactivoId { get; set; }
        public string UnidadSi { get; set; }
        public string Fcsi { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioId { get; set; }
        public IEnumerable<ParameterStudyDto> Estudios { get; set; }
        public string Area { get; set; }
        public string Departamento { get; set; }
        public string Reactivos { get; set; }
        public string Format { get; set; }
    }

    public class ParameterFormDtoValidator : AbstractValidator<ParameterFormDto>
    {
        public ParameterFormDtoValidator()
        {
            RuleFor(x => x.Clave).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.ValorInicial).NotEmpty();
            RuleFor(x => x.NombreCorto).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Unidades).GreaterThan(0);
            RuleFor(x => x.TipoValor).NotEmpty();
            RuleFor(x => x.DepartamentoId).GreaterThan(0);
            RuleFor(x => x.AreaId).GreaterThan(0);
            RuleFor(x => x.FormatoImpresionId).GreaterThan(0);
            RuleFor(x => x.ReactivoId).Guid();
            RuleFor(x => x.Formula).MaximumLength(200);
            RuleFor(x => x.UnidadSi).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Fcsi).NotEmpty().MaximumLength(50);
        }
    }
}
