using System;

namespace Service.Identity.Dtos
{
    public class BranchInfo
    {
        public Guid IdSucursal { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public long? Telefono { get; set; }
        public string CodigoPostal { get; set; }
        public string Ubicacion { get; set; }
        public string Clinico { get; set; }
        public bool Activo { get; set; }
    }
}
