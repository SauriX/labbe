using System;

namespace Identidad.Api.Model.Medicos
{
    public class MedicoClinica
    {
        public int IdMedico_Clinica { get; set; }
        public long MedicoId { get; set; }
        public long ClinicaId { get; set; }
        public bool Activo { get; set; }
        public long UsuarioCreoId { get; set; }
        public string UsuarioCreo  { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
        
    }
}
