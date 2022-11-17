using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.ResultValidation
{
    public class ValidationListDto
    {
        public Guid Id { get; set; }
        public string Solicitud { get; set; }
        public string Nombre { get; set; }
        public string Registro { get; set; }
        public string Sucursal { get; set; }
        public string Edad { get; set; }
        public string Sexo { get; set; }
        public string Compañia { get; set; }
        public List<ValidationStudyDto> Estudios { get; set; }
        public string Order { get; set; }
    }
}
