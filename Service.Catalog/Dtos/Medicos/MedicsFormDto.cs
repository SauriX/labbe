using FluentValidation;
using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Medicos
{
    public class MedicsFormDto
    {
        public int IdMedico { get; set; }
        public string Clave { get; set; }
        public bool ClaveCambio { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public int EspecialidadId { get; set; }
        public string Observaciones { get; set; }
        public int CodigoPostal { get; set; }
        public long? EstadoId { get; set; }
        public long? CiudadId { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Calle { get; set; }
        public int ColoniaId { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
        public Guid IdUsuario { get; set; }
        public IEnumerable<CatalogListDto> Clinicas { get;set;}
    }

    public class MedicosFormDtoValidator : AbstractValidator<MedicsFormDto>
    {
        public MedicosFormDtoValidator()
        {
            RuleFor(x => x.IdMedico).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Clave).NotEmpty().MaximumLength(15);//.NotEqual();
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(50);
            RuleFor(x => x.PrimerApellido).NotEmpty().MaximumLength(50);
            RuleFor(x => x.SegundoApellido).NotEmpty().MaximumLength(50);
            RuleFor(x => x.EspecialidadId).NotEmpty();
            RuleFor(x => x.Calle).NotEmpty().MaximumLength(50);
            RuleFor(x => x.ColoniaId).NotEmpty();
           
        }
        
    }
}
