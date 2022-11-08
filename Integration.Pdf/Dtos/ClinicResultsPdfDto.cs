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
        public string UltimoResultado { get; set; }
        public decimal? CriticoMinimo { get; set; }
        public decimal? CriticoMaximo { get; set; }
        public bool DeltaCheck { get; set; }
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
    }
}