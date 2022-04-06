using Identidad.Api.Model.Medicos;
using System;
using System.Collections.Generic;

namespace Identidad.Api.ViewModels.Menu
{
    public class Medics
    {
        public int IdMedico { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public long EspecialidadId { get; set; }
        public string Observaciones { get; set; }
        public int CodigoPostal { get; set; }
        public long? EstadoId { get; set; }
        public long? CiudadId { get; set; }
        public int NumeroExterior { get; set; }
        public int? NumeroInterior { get; set; }
        public string Calle { get; set; }
        public long ColoniaId { get; set; }
        public string Correo { get; set; }
        public long? Celular { get; set; }
        public long? Telefono { get; set; }
        public bool Activo { get; set; }
        public int UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public int UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }

        public virtual ICollection<MedicClinic> Clinicas { get; set; } = new List<MedicClinic>();
    }
}