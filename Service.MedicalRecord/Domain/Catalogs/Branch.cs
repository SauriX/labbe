using System;

namespace Service.MedicalRecord.Domain.Catalogs
{
    public class Branch
    {
        public Guid Id { get; set; }
        public string CodigoPostal { get; set; }
        public short CiudadId { get; set; }
        public string Clinicos { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
    }
}
