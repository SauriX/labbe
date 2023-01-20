using Service.MedicalRecord.Domain;
using System;

namespace Service.Catalog.Domain.Series
{
    public class Series : BaseModel
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public byte TipoSerie { get; set; }
        public Guid SucursalId { get; set; }
        public bool Activo { get; set; }
        public virtual Branch.Branch Sucursal { get; set; }
    }
}
