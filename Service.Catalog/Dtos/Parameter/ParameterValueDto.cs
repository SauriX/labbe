using FluentValidation;
using System;

namespace Service.Catalog.Dtos.Parameters
{
    public class ParameterValueDto
    {
        public string Id { get; set; }
        public string ParametroId { get; set; }
        public string Nombre { get; set; }
        public decimal ValorInicial { get; set; }
        public decimal ValorFinal { get; set; }
        public decimal ValorInicialNumerico { get; set; }
        public decimal ValorFinalNumerico { get; set; }
        public int RangoEdadInicial { get; set; }
        public int RangoEdadFinal { get; set; }
        public decimal HombreValorInicial { get; set; }
        public decimal HombreValorFinal { get; set; }
        public decimal MujerValorInicial { get; set; }
        public decimal MujerValorFinal { get; set; }
        public byte MedidaTiempoId { get; set; }
        public string Opcion { get; set; }
        public string DescripcionTexto { get; set; }
        public string DescripcionParrafo { get; set; }
        public Guid UsuarioId { get; set; }
    }
    public class ParametervalueDtoValidator : AbstractValidator<ParameterValueDto>
    {
        public ParametervalueDtoValidator()
        {
            RuleFor(x => x.ValorInicial).GreaterThanOrEqualTo(0);
            RuleFor(x => x.ValorFinal).GreaterThanOrEqualTo(0);
            RuleFor(x => x.ValorInicialNumerico).GreaterThanOrEqualTo(0);
            RuleFor(x => x.ValorFinalNumerico).GreaterThanOrEqualTo(0);
            RuleFor(x => x.RangoEdadInicial).GreaterThanOrEqualTo(0);
            RuleFor(x => x.RangoEdadFinal).GreaterThanOrEqualTo(0);
            RuleFor(x => x.HombreValorInicial).GreaterThanOrEqualTo(0);
            RuleFor(x => x.HombreValorFinal).GreaterThanOrEqualTo(0);
            RuleFor(x => x.MujerValorInicial).GreaterThanOrEqualTo(0);
            RuleFor(x => x.MujerValorFinal).GreaterThanOrEqualTo(0);

        }
    }
}
