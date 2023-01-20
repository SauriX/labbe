namespace Service.Billing.Dtos.Series
{
    public class SeriesListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string Sucursal { get; set; }
        public string TipoSerie { get; set; }
        public bool Activo { get; set; }
    }
}
