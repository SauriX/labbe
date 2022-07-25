using System;

namespace EventBus.Messages.Catalog
{
    public class BranchContract
    {
        public BranchContract()
        {

        }

        public BranchContract(Guid id, string clave, string nombre, string clinicos)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            Clinicos = clinicos;
        }

        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Clinicos { get; set; }
    }
}
