using System;

namespace Service.MedicalRecord.Domain.TrackingOrder
{
    public class TrackingOrderDetail
    {
        public Guid Id { get; set; }
        public virtual TrackingOrder trackingOrder { get; set; }
        public Guid SeguimientoId { get; set; }
        public Guid RutaId { get; set; }
        public string NombreRuta { get; set; }
        public string SucursalDestinoId { get; set; }
        public string SucursalOrigenId { get; set; }
        public Guid ResponsableEnvio { get; set; }
        public int NumeroRastreo { get; set; }
        public Guid MaquiladorId { get; set; }
        public string Correo { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
        public string PaginaWeb { get; set; }
        public DateTime FechaEntregaEstimada { get; set; }
        public DateTime HoraEntregaEstimada { get; set; }
        public DateTime FechaEntregaReal { get; set; }
        public DateTime HoraEntregaReal { get; set; }
        public Guid MedioDeEntregaId { get; set; }
        public Guid EstudioId { get; set; }
        public Guid PacienteId { get; set; }
        public Guid SolicitudId { get; set; }
        public bool Escaneado { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }

    }
}
