using System;

namespace Service.MedicalRecord.Dtos.MedicalRecords
{
    public class MedicalRecordsFormDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Expediente { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Cp { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Celular { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public Guid UserId { get; set; }
    }
}
