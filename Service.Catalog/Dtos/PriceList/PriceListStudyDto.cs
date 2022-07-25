using FluentValidation;
using Service.Catalog.Dtos.Pack;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.PriceList
{
    public class PriceListStudyDto
    {
        public int Id { get; set; }
        public int EstudioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        //public int AreaId { get; set; }
        public string Area { get; set; }
        public string Departamento { get; set; }
        public bool Activo { get; set; }
        public decimal Precio { get; set; }
        public IEnumerable<PackStudyDto> Pack { get; set; }
        public class PriceListStudyDtoValidator : AbstractValidator<PriceListStudyDto>
        {
            public PriceListStudyDtoValidator()
            {
                RuleFor(x => x.Precio).GreaterThan(0);




            }

        }
    }
}
