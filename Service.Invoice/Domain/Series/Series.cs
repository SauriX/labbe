using System;

namespace Service.Billing.Domain.Series
{
    public class Series : BaseModel
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public string Descripcion { get; set; }
        public byte TipoSerie { get; set; }
        public Guid SucursalId { get; set; }
        public string Sucursal { get; set; }
        public string Ciudad { get; set; }
        public bool Activo { get; set; }
        public bool CFDI { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid EmisorId { get; set; }
        public string ArchivoKey { get; set; }
        public string ArchivoCer { get; set; }
        public string SucursalKey { get; set; }
    }
}
