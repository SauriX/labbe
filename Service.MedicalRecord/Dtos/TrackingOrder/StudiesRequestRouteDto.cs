using System;

namespace Service.MedicalRecord.Dtos.TrackingOrder
{
    public class StudiesRequestRouteDto
    {
        public string Clave { get; set; }
        public Guid SolicitudId { get; set; }

        public string Solicitud { get; set; }
        public int EstudioId { get; set; }
        public string Estudio { get; set; }
        public Guid ExpedienteId { get; set; }
        public string NombrePaciente { get; set; }
        public decimal Temperatura { get; set; }
        public bool Escaneado { get; set; }
        public bool IsExtra { get; set; }
        public string TaponNombre { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechoCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
