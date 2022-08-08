using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Catalog
{
    public class CompanyContract
    {
        public CompanyContract()
        {
        }

        public CompanyContract(Guid id, string clave, string nombre)
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
