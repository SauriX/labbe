using System;

namespace Service.MedicalRecord.Dtos.TrackingOrder
{
    public class StudyRouteDto
    {
        public Guid Id { get; set; }
        public int EtiquetaId { get; set; }
        public Guid SeguimientoId { get; set; }
        public Guid SolicitudId { get; set; }
        public string Solicitud { get; set; }
        public string ClaveEtiqueta { get; set; }
        public string Recipiente { get; set; }
        public decimal Cantidad { get; set; }
        public string Estudios { get; set; }
        public string ClaveRuta { get; set; }
        public byte Estatus { get; set; }
        public bool Escaneo { get; set; }
        public bool Extra { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechoCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
