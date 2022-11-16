using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.MassSearch
{
    public class DeliverResultsStudiesDto
    {
        public List<DeliverResultStudieDto> Estudios { get; set; }
        public Guid UsuarioId { get; set; }
        public string Usuario { get; set; }
        public List<string> MediosEnvio { get; set; }
    }
    public class DeliverResultStudieDto
    {
        public Guid SolicitudId { get; set; }
        public List<SolicitudEstudioDto> EstudiosId { get; set; }
    }
    public class SolicitudEstudioDto
    {
        public int EstudioId { get; set; }
        public int Tipo { get; set; }
    }
}
