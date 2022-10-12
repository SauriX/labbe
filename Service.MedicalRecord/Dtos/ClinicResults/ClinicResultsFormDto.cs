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
        public int TipoValorId { get; set; }
        public decimal ValorInicial { get; set; }
        public decimal ValorFinal { get; set; }
        public string Resultado { get; set; }
        public byte Estatus { get; set; }
        public string DepartamentoEstudio { get; set; }
        public Guid UsuarioId { get; set; }
        public string UnidadNombre { get; set; }
    }
}
