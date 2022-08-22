using System;

namespace Service.MedicalRecord.Domain.Request
{
    public class RequestImage : BaseModel
    {
        public RequestImage()
        {
        }

        public RequestImage(int id, Guid solicitudId, string clave, string ruta, string tipo)
        {
            Id = id;
            SolicitudId = solicitudId;
            Clave = clave;
            Ruta = ruta;
            Tipo = tipo;
        }

        public int Id { get; set; }
        public Guid SolicitudId { get; set; }
        public virtual Request Solicitud { get; set; }
        public string Clave { get; set; }
        public string Ruta { get; set; }
        public string Tipo { get; set; }
    }
}
