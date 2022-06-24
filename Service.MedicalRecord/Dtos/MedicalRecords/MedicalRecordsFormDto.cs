using System;
using System.Collections.Generic;

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
        public int Colonia { get; set; }
        public string sucursal { get; set; }
        public Guid UserId { get; set; }

#pragma warning disable CS8632 // La anotación para tipos de referencia que aceptan valores NULL solo debe usarse en el código dentro de un contexto de anotaciones "#nullable".
        public IEnumerable<TaxDataDto>? TaxData { get; set; }
#pragma warning restore CS8632 // La anotación para tipos de referencia que aceptan valores NULL solo debe usarse en el código dentro de un contexto de anotaciones "#nullable".
    } 
}
