namespace Service.Catalog.Domain.Constant
{
    public class Colony
    {
        public int Id { get; set; }
        public short CiudadId { get; set; }
        public virtual City Ciudad { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }
    }
}
