using System;

namespace Service.MedicalRecord.Dtos.ClinicResults
{
    public class ClinicResultsUpdateDto
    {
        public int EstudioId { get; set; }
        public Guid SolicitudId { get; set; }
    }
}
