using System;

namespace Service.Catalog.Domain.Parameter
{
    public class TipoValor
    {
        public Guid IdTipo_Valor { get; set; }
        public Guid IdParametro { get; set; }
        public virtual Parameters parametres { get; set; }
        public string Nombre { get; set; }
        public string ValorInicial { get; set; }
        public string ValorFinal { get; set; }
        public string ValorInicialNumerico { get; set; }
        public string ValorFinalNumerico { get; set; }
        public string RangoEdadInicial { get; set; }
        public string RangoEdadFinal { get; set; }
        public string HombreValorInicial { get; set; }
        public string HombreValorFinal { get; set; }
        public string MujerValorInicial { get; set; }
        public string MujerValorFinal { get; set; }
        public string MedidaTiempo { get; set; }
        public string Opcion { get; set; }
        public string DescripcionTexto { get; set; }
        public string DescripcionParrafo { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }

    }
}
