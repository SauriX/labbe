﻿using FluentValidation;
using System;

namespace Service.Catalog.Dtos.Maquilador
{
    public class MaquiladorFormDto
    {
        public int Id { set; get; }
        public string Clave { set; get; }
        public string Nombre { set; get; }
        public string Correo { set; get; }
        public string Telefono { set; get; }
        public string PaginaWeb { set; get; }
        public int CodigoPostal { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Calle { get; set; }
        public int ColoniaId { get; set; }
        public string Colonia { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public bool Activo { get; set; }
        public string UsuarioId { get; set; }
    }
    public class MaquiladorFormDtoValidator : AbstractValidator<MaquiladorFormDto>
    {
        public MaquiladorFormDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Clave).NotEmpty().MaximumLength(15);
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Correo).MaximumLength(100);
            RuleFor(x => x.PaginaWeb).MaximumLength(500);
        }
    }
}
