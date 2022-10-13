﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Catalog.Domain.Configuration
{
    public class Configuration
    {
        public Configuration()
        {
        }

        public Configuration(int id, string descripcion, string valor = null)
        {
            Id = id;
            Descripcion = descripcion;
            Valor = valor;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }
    }
}
