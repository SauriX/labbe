using System;

namespace Service.MedicalRecord.Domain.Catalogs
{
    public class Branch
    {
        public Branch()
        {
        }

        public Branch(Guid id, string codigo, string clave, string nombre, string clinicos, string codigoPostal, short ciudadId)
        {
            Id = id;
            Codigo = codigo;
            Clave = clave;
            Nombre = nombre;
            Clinicos = clinicos;
            CodigoPostal = codigoPostal;
            CiudadId = ciudadId;
        }

        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Clinicos { get; set; }
        public string CodigoPostal { get; set; }
        public short CiudadId { get; set; }
    }
}
