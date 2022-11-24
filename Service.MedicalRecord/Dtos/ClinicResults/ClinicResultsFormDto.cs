using System;
using System.Collections.Generic;
using Service.MedicalRecord.Dtos.Catalogs;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.Sampling;

namespace Service.MedicalRecord.Dtos.ClinicResults
{
    public class ClinicResultsFormDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public Guid SolicitudId { get; set; }
        public int EstudioId { get; set; }
        public string ParametroId { get; set; }
        public int SolicitudEstudioId { get; set; }
        public string Estudio { get; set; }
        public string TipoValorId { get; set; }
        public string ValorInicial { get; set; }
        public string ValorFinal { get; set; }
        public decimal? CriticoMinimo { get; set; }
        public decimal? CriticoMaximo { get; set; }
        public string Resultado { get; set; }
        public string Formula { get; set; }
        public string NombreCorto { get; set; }
        public byte Estatus { get; set; }
        public string DepartamentoEstudio { get; set; }
        public Guid UsuarioId { get; set; }
        public string Usuario { get; set; }
        public string UsuarioClave { get; set; }
        public string UnidadNombre { get; set; }
        public string UltimoResultado { get; set; }
        public bool DeltaCheck { get; set; }
        public int Orden { get; set; }
        public string Clave { get; set; }
    }
}
