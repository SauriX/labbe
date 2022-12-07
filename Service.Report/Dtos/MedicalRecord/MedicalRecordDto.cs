using System;

namespace Service.Report.Dtos.MedicalRecord
{
    public class MedicalRecordDto
    {
        public Guid Id { get; set; }
        public string Expediente { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
    }
}
