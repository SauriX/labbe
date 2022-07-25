namespace Service.Catalog.Dtos.Branch
{
    public class BranchInfoDto
    {
        public string idSucursal { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string ubicacion { get; set; }
        public string clinico { get; set; }
        public bool activo { get; set; }
        public string codigoPostal { get; set; }
    }
}
