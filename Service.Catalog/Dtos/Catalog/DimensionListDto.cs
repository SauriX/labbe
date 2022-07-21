namespace Service.Catalog.Dtos.Catalog
{
    public class DimensionListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public byte Largo { get; set; }
        public byte Ancho { get; set; }
        public bool Activo { get; set; }
    }
}
