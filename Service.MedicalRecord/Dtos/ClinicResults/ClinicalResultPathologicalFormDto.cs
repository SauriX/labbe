using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.ClinicResults
{
    public class ClinicalResultPathologicalFormDto
    {
        public Guid Id { get; set; }
        public Guid SolicitudId { get; set; }
        public int EstudioId { get; set; }
        public int RequestStudyId  { get; set; }
        public string DescripcionMacroscopica { get; set; }
        public string DescripcionMicroscopica { get; set; }
        public List<IFormFile> ImagenPatologica { get; set; }
        public string Diagnostico { get; set; }
        public string MuestraRecibida { get; set; }
        public Guid? MedicoId { get; set; }
        public string? NombreMedico { get; set; }
        public string[] ListaImagenesCargadas { get; set; }
        public byte Estatus { get; set; }
        public string  DepartamentoEstudio { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
