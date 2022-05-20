﻿using System;

namespace Service.Catalog.Domain.Company
{
    public class Contact
    {
        public int Id { get; set; }
        public Guid CompañiaId { get; set; }
        public virtual Company Compañia { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public long? Telefono { get; set; }
        public string Correo { get; set; }
        public bool Activo { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}