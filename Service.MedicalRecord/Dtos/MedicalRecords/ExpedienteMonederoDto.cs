using System;

namespace Service.MedicalRecord.Dtos.MedicalRecords
{
    public class ExpedienteMonederoDto
    {
        public Guid Id { get; set; }
        public decimal Saldo { get; set; }
        public bool Activo { get; set; }
    }
}
