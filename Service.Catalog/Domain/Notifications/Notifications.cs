using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Notifications
{
    public class Notifications
    {

        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public bool IsNotifi { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public ICollection<Notification_Branch> Sucursales { get; set; }
        public ICollection<Notification_Role> Roles { get; set; }
        public bool Lunes { get; set; }
        public bool Martes { get; set; }
        public bool Miercoles { get; set; }
        public bool Jueves { get; set; }
        public bool Viernes { get; set; }
        public bool Sabado { get; set; }
        public bool Domingo { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public string UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
        public string Tipo { get; set; }
    }
}
