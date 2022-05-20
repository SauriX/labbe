namespace Service.Catalog.Dtos.Promotion
{
    public class PromotionListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Periodo { get; set; }
        public string NombreListaPrecio { get; set; }
        public bool Activo { get; set; }
    }
}
