using System;
namespace Identidad.Api.ViewModels.Medicos
{
    public class MedicsFormDto
    {
        public int IdMedico { get; set; }
        public string Clave { get; set; }
        public string NombreCompleto { get; set; }
        public long EspecialidadId { get; set; }
        public string Observaciones { get; set; }
        public string Direccion { get; set; }      
        public string Clinica { get; set; }
        public string Correo { get; set; }
        public int Celular { get; set; }
        public int Telefono { get; set; }
        public bool Activo { get; set; }
 
    }
}
