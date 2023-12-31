﻿using FluentValidation;
using Service.Catalog.Dtos.Reagent;
using Shared.Validators;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Parameter
{
    public class ParameterFormDto
    {
        public string Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public int? Unidades { get; set; }
        public string UnidadNombre { get; set; }
        public string TipoValor { get; set; }
        public string Formula { get; set; }
        public string ValorInicial { get; set; }
        public int? DepartamentoId { get; set; }
        public int? AreaId { get; set; }
        public int FormatoImpresionId { get; set; }
        public string ReactivoId { get; set; }
        public int? UnidadSi { get; set; }
        public string UnidadSiNombre { get; set; }
        public string Fcsi { get; set; }
        public bool Activo { get; set; }
        public bool Requerido { get; set; }
        public bool DeltaCheck { get; set; }
        public bool MostrarFormato { get; set; }
        public bool ValoresCriticos { get; set; }
        public Guid UsuarioId { get; set; }
        public IEnumerable<ParameterStudyDto> Estudios { get; set; }
        public IEnumerable<ReagentListDto> Reactivos { get; set; }
        public string Area { get; set; }
        public string Departamento { get; set; }
        public string Format { get; set; }
    }

    public class ParameterFormDtoValidator : AbstractValidator<ParameterFormDto>
    {
        public ParameterFormDtoValidator()
        {
            RuleFor(x => x.Clave).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.TipoValor).NotEmpty();
            RuleFor(x => x.ReactivoId).Guid();
            RuleFor(x => x.Formula).MaximumLength(200);

        }
    }
}
