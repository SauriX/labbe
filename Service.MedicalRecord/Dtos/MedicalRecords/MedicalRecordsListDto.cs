using System;

namespace Service.MedicalRecord.Dtos.MedicalRecords
{
    public class MedicalRecordsListDto
    {
        public Guid Id { get; set; }
        public string Expediente { get; set; }
        public string NomprePaciente { get; set; }
        public string Genero { get; set; }
        public int Edad { get; set; }
        public string FechaNacimiento { get; set; }
        public decimal MonederoElectronico { get; set; }
        public bool MonederoActivo { get; set; }
        public Guid SucursalId { get; set; }
        public string Sucursal { get; set; }
        public string Telefono { get; set; }
    }
}
