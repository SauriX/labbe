using System;

namespace Service.MedicalRecord.Dtos.TrackingOrder
{
    public class StudiesRequestRouteDto
    {
        public string Clave { get; set; }
        public string SolicitudId { get; set; }
        public string EstudioId { get; set; }
        public string Estudio { get; set; }
        public string PacienteId { get; set; }
        public string NombrePaciente { get; set; }
        public decimal Temperatura { get; set; }
        public bool Escaneado { get; set; }
        public string TaponNombre { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechoCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
