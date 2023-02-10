
using System;

namespace Service.MedicalRecord.Domain
{
    public class DeliveryHistory: BaseModel
    {
      
            public Guid Id { get; set; }
            public string Numero { get; set; }
            public string Correo { get; set; }
            public Guid SolicitudId { get; set; }
            public int SolicitudEstudioId { get; set; }
            public string Descripcion { get; set; }
            public string UsuarioNombre { get; set; }

    }
}
