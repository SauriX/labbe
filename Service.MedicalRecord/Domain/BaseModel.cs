using System;

namespace Service.MedicalRecord.Domain
{
    public class BaseModel
    {
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid? UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
    }
}
