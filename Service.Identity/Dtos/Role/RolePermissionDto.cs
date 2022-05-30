using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Identity.Dtos.Role
{
    public class RolePermissionDto
    {
        public RolePermissionDto() { }

        public RolePermissionDto(int id, short menuId, string menu, string permiso, bool asignado, byte tipo)
        {
            Id = id;
            MenuId = menuId;
            Menu = menu;
            Permiso = permiso;
            Asignado = asignado;
            Tipo = tipo;
        }

        public int Id { get; set; }
        public short MenuId { get; set; }
        public string Menu { get; set; }
        public string Permiso { get; set; }
        public bool Asignado { get; set; }
        public int Tipo { get; set; }
    }
}
