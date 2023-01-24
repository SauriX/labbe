namespace Service.MedicalRecord.Dtos.Series
{
    public class SeriesDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string Sucursal { get; set; }
        public string TipoSerie { get; set; }
        public bool Activo { get; set; }
        public bool CFDI { get; set; }
        public string Año { get; set; }
    }
}
