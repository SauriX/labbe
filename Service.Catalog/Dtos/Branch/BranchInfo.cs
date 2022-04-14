namespace Service.Catalog.Dtos.Branch
{
    public class BranchInfo
    {
        public string idSucursal { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public int telefono { get; set; }
        public string ubicacion { get; set; }
        public string clinico { get; set; }
        public string activo {get; set;}
        public string codigoPostal {get; set;}
    }
}
