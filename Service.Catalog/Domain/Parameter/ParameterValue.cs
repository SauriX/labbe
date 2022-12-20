using System;

namespace Service.Catalog.Domain.Parameter
{
    public class ParameterValue
    {
        public ParameterValue()
        {
            Activo = true;
            FechaCreo = DateTime.Now;
        }

        public ParameterValue(Guid id, Guid parametroId, decimal inicial, decimal final, string nombre)
        {
            Id = id;
            ParametroId = parametroId;
            ValorInicial = inicial;
            ValorFinal = final;
            Nombre = nombre;
            Activo = true;
            FechaCreo = DateTime.Now;
        }

        public Guid Id { get; set; }
        public Guid ParametroId { get; set; }
        public virtual Parameter Parametro { get; set; }
        public string Nombre { get; set; }
        public decimal ValorInicial { get; set; }
        public decimal ValorFinal { get; set; }
        public decimal CriticoMinimo { get; set; }
        public decimal CriticoMaximo { get; set; }
        public decimal CriticoMinimoHombre { get; set; }
        public decimal CriticoMaximoHombre { get; set; }
        public decimal CriticoMinimoMujer { get; set; }
        public decimal CriticoMaximoMujer { get; set; }
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
        public string PrimeraColumna { get; set; }
        public string SegundaColumna { get; set; }
        public string TerceraColumna { get; set; }
        public string CuartaColumna { get; set; }
        public string QuintaColumna { get; set; }
        public int Orden { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModificoId { get; set; }
        public DateTime FechaModifico { get; set; }
    }
}
