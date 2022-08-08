using System;

namespace Service.MedicalRecord.Domain.Catalogs
{
    public class Company
    {
        public Company()
        {
        }

        public Company(Guid id, string clave, string nombre)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
        }

        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
    }
}
