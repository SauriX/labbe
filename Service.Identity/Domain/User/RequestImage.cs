using System;

namespace Service.Identity.Domain.User
{
    public class RequestImage 
    {
        public RequestImage()
        {
        }

        public RequestImage(int id, Guid solicitudId, string clave, string ruta, string tipo)
        {
            Id = id;
            UserId = solicitudId;
            Clave = clave;
            Ruta = ruta;
            Tipo = tipo;
        }

        public int Id { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string Clave { get; set; }
        public string Ruta { get; set; }
        public string Tipo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid? UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
    }
}
