using System;

namespace Service.MedicalRecord.Dtos.MedicalRecords
{
    public class MedicalRecordsListDto
    {
        public Guid Id { get; set; }
        public string Expediente { get; set; }
        public string NombrePaciente { get; set; }
        public string Genero { get; set; }
        public int Edad { get; set; }
        public string FechaNacimiento { get; set; }
        public decimal MonederoElectronico { get; set; }
        public bool MonederoActivo { get; set; }
        public Guid SucursalId { get; set; }
        public string Sucursal { get; set; }
        public string Telefono { get; set; }
        public string FechaAlta { get; set; }
    }
}
