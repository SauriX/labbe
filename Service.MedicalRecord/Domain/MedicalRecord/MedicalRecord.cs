using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Domain.MedicalRecord
{
    public class MedicalRecord
    {
        public Guid Id { get; set; }
        public string Expediente { get; set; }
        public string NombrePaciente { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string NombreCompleto => NombrePaciente + " " + PrimerApellido + " " + SegundoApellido;
        public string Genero { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string CodigoPostal { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string Celular { get; set; }
        public string Calle { get; set; }
        public int? ColoniaId { get; set; }
        public decimal Monedero { get; set; }
        public bool MonederoActivo { get; set; }
        public DateTime FechaActivacionMonedero { get; set; }
        public Guid IdSucursal { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }

        public IEnumerable<MedicalRecordTaxData> TaxData { get; set; }
    }
}
