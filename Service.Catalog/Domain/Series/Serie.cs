using Service.MedicalRecord.Domain;
using System;

namespace Service.Catalog.Domain.Series
{
    public class Serie : BaseModel
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public string Descripcion { get; set; }
        public byte TipoSerie { get; set; }
        public Guid SucursalId { get; set; }
        public virtual Branch.Branch Sucursal { get; set; }
        public string Ciudad { get; set; }
        public bool Activo { get; set; }
        public bool CFDI { get; set; }
        public DateTime FechaCreo { get; set; }
        public string ArchivoKey { get; set; }
        public string ArchivoCer { get; set; }
        public string SucursalKey { get; set; }
        public bool Relacion { get; set; }
    }
}
