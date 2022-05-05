using System;

namespace Service.Catalog.Dtos.Parameters
{
    public class ParameterValueDto
    {
        public string Id { get; set; }
        public string ParametroId { get; set; }
        public byte TipoValorId { get; set; }
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
}
