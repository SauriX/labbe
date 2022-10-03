using System;

namespace Service.MedicalRecord.Dtos.ClinicResults
{
    public class UpdateStatusDto
    {
        public int RequestStudyId { get; set; }
        public byte status { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
