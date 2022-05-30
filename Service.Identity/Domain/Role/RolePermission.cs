using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Identity.Domain.Role
{
    public class RolePermission
    {
        public RolePermission() { }

        public RolePermission(Guid rolId, short menuId, bool acceder, bool crear, bool modificar, bool imprimir, bool descargar, bool enviarCorreo, bool enviarWapp)
        {
            RolId = rolId;
            MenuId = menuId;
            Acceder = acceder;
            Crear = crear;
            Modificar = modificar;
            Imprimir = imprimir;
            Descargar = descargar;
            EnviarCorreo = enviarCorreo;
            EnviarWapp = enviarWapp;
        }

        public Guid RolId { get; set; }
        public virtual Role Rol { get; set; }
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
