using FluentValidation;
using Service.Catalog.Domain.Price;
using Service.Catalog.Dtos.PriceList;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Promotion
{
    public class PromotionFormDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string TipoDescuento { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
        public string IdListaPrecios { get; set; }
        public bool Lealtad { get; set; }
        public Guid UsuarioId { get; set; }
        public IEnumerable<PromotionEstudioListDto> Estudio {get; set;}
        public List<PriceListBranchDto> Branchs { get; set; }
        public string lista { get; set; }
        public IEnumerable<DiasDto> Dias { get; set; }

    }

    public class PromotionFormDtoValidator : AbstractValidator<PromotionFormDto>
    {
        public PromotionFormDtoValidator()
        {
            RuleFor(x => x.Dias).NotEmpty(); ;
            
        }
    }
}
