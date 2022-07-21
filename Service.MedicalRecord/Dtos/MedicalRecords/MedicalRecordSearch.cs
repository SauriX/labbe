using System;

namespace Service.MedicalRecord.Dtos.MedicalRecords
{
    public class MedicalRecordSearch
    {
        public string expediente { get; set; }
        public string telefono { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public DateTime fechaAlta { get; set; }
        public string ciudad { get; set; }
        public string sucursal { get; set; }
    }
}
