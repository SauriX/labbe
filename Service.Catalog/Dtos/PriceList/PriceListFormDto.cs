using FluentValidation;
using Service.Catalog.Domain.Company;
using Service.Catalog.Domain.Price;
using Service.Catalog.Dtos.Promotion;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos
{
    public class PriceListFormDto
    {
        public string Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public bool? Visibilidad { get; set; }
        public bool Activo { get; set; }        
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
        public virtual IEnumerable<PromotionFormDto> Promocion { get; set; }
        public virtual ICollection<Price_Company> Compañia { get; set; }
        public virtual ICollection<PriceList_Study> Estudios { get; set; }
        public virtual ICollection<PriceList_Packet> Paquete { get; set; }
        public virtual ICollection<Price_Branch> Sucursales { get; set; }
        public virtual ICollection<Price_Medics> Medicos { get; set; }
    }
    public class PriceListFormDtoValidator : AbstractValidator<PriceListFormDto>
    {
        public PriceListFormDtoValidator()
        {
            RuleFor(x => x.Clave).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        }
    }
}