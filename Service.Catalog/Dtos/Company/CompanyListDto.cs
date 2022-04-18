namespace Service.Catalog.Dtos.Company
{
    public class CompanyListDto
    {
        public int IdCompania { get; set; }
        public string Clave { get; set; }
        public string Contrasena { get; set; }
        public int Procedencia { get; set; }
        public int ListaPrecioId { get; set; }
        public bool Activo { get; set; }

    }
}