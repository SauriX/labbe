namespace Service.Catalog.Dtos.PriceList
{
    public class PriceListStudyDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Area { get; set; }
        public int DepartamentoId { get; set; }
        public bool Activo { get; set; }
        public float Precio { get; set; }
    }
}
