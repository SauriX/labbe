using System;

namespace Service.MedicalRecord.Domain.MedicalRecord
{
    public class MedicalRecord
    {
        public Guid Id { get; set; }
        public string Expediente { get; set; }
        public string NombrePaciente { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
     /*   public Guid IdSolicitud { get; set; }
        public Guid IdCotizacion { get; set; }*/
        public string Genero { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public int CodigoPostal { get; set; }
        public int EstadoId { get; set; }
        public int CiudadId { get; set; }
        public int Celular { get; set; }
        public string Calle { get; set; }
        public int ColoniaId { get; set; }
        public int Monedero { get; set; }
        public Guid IdSucursal { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
