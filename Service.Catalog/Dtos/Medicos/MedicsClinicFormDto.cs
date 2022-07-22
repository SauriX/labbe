using System;

namespace Service.Catalog.Dtos.Medicos
{
    public class MedicsClinicFormDto
    {
        public int IdMedico_Clinica { get; set; }
        public long MedicoId { get; set; }
        public long ClinicaId { get; set; }
        public bool Activo { get; set; }
        public long UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public long UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }

    }
}
