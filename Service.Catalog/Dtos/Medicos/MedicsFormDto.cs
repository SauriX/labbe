using System;
namespace Identidad.Api.ViewModels.Medicos
{
    public class MedicsFormDto
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
        public int IdUsuario { get; set; }

    }
}
