using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Dtos
{
    public class ClinicResultsPdfDto
    {
        public IEnumerable<ClinicResultsCaptureDto> CapturaResultados { get; set; }
        public ClinicResultsRequestDto SolicitudInfo { get; set; }
        public bool ImprimrLogos { get; set; }
        public bool ImprimirCriticos { get; set; }
        public bool ImprimirPrevios { get; set; }
    }

    public class ClinicResultsCaptureDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public Guid SolicitudId { get; set; }
        public int EstudioId { get; set; }
        public string Estudio { get; set; }
        public string ParametroId { get; set; }
        public int TipoValorId { get; set; }
        public string UnidadNombre { get; set; }
        public string ValorInicial { get; set; }
        public string ValorFinal { get; set; }
        public string Resultado { get; set; }
        public string Clave { get; set; }
        public string UltimoResultado { get; set; }
        public decimal? CriticoMinimo { get; set; }
        public decimal? CriticoMaximo { get; set; }
        public bool ImprimirCriticos { get; set; }
        public bool ImprimirPrevios { get; set; }
        public int Orden { get; set; }
        public string FCSI { get; set; }
        public List<ParameterValueDto> ValoresReferencia { get; set; }
    }

    public class ClinicResultsRequestDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Medico { get; set; }
        public string Paciente { get; set; }
        public string Compañia { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Expediente { get; set; }
        public string FechaAdmision { get; set; }
        public string FechaEntrega { get; set; }
        public string User { get; set; }
        public string Metodo { get; set; }
    }

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
        public decimal CriticoMinimo { get; set; }
        public decimal CriticoMaximo { get; set; }
        public decimal HombreCriticoMinimo { get; set; }
        public decimal HombreCriticoMaximo { get; set; }
        public decimal MujerCriticoMinimo { get; set; }
        public decimal MujerCriticoMaximo { get; set; }
        public byte MedidaTiempoId { get; set; }
        public string Opcion { get; set; }
        public string DescripcionTexto { get; set; }
        public string DescripcionParrafo { get; set; }
        public string PrimeraColumna { get; set; }
        public string SegundaColumna { get; set; }
        public string TerceraColumna { get; set; }
        public string CuartaColumna { get; set; }
        public string QuintaColumna { get; set; }
    }
}