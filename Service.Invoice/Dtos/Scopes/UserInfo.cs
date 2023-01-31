using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Billing.Dtos.Scopes
{
    public class UserInfo
    {
        public string Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public Guid RolId { get; set; }
        public string Rol { get; set; }
        public Guid SucursalId { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
