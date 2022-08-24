using System;

namespace Service.Report.Domain.Catalogs
{
    public class Maquila
    {
        public Maquila()
        {
        }

        public Maquila(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
