using System;

namespace Service.MedicalRecord.Domain.RouteTracking
{
    public class RouteTracking
    { 
      public Guid Id { get; set; }
      public Guid SegumientoId { get; set; }
      public Guid RutaId { get; set; }
      public Guid SucursalId { get; set; }
      public DateTime FechaDeEntregaEstimada { get; set; }
      //EstudioId
      public Guid  SolicitudId { get; set; }
      public virtual Request.Request Solicitud { get; set; }
      public DateTime  HoraDeRecoleccion { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModificoId { get; set; }    
        public DateTime? FechaModifico { get; set; }

    }
}
