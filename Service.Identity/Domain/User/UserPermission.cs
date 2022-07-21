using System;

namespace Service.Identity.Domain.User
{
    public class UserPermission
    {
        public UserPermission() { }

        public UserPermission(Guid usuarioId, short menuId, bool acceder, bool crear, bool modificar, bool imprimir, bool descargar, bool enviarCorreo, bool enviarWapp)
        {
            UsuarioId = usuarioId;
            MenuId = menuId;
            Acceder = acceder;
            Crear = crear;
            Modificar = modificar;
            Imprimir = imprimir;
            Descargar = descargar;
            EnviarCorreo = enviarCorreo;
            EnviarWapp = enviarWapp;
        }

        public Guid UsuarioId { get; set; }
        public virtual User Usuario { get; set; }
        public short MenuId { get; set; }
        public virtual Menu.Menu Menu { get; set; }
        public bool Acceder { get; set; }
        public bool Crear { get; set; }
        public bool Modificar { get; set; }
        public bool Imprimir { get; set; }
        public bool Descargar { get; set; }
        public bool EnviarCorreo { get; set; }
        public bool EnviarWapp { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
    }
}
