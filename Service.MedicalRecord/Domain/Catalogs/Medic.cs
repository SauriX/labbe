using System;

namespace Service.MedicalRecord.Domain.Catalogs
{
    public class Medic
    {
        public Medic()
        {
        }

        public Medic(Guid id, string clave, string nombre)
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
