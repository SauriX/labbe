using Service.MedicalRecord.Domain.Request;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Sampling
{
    public class SamplingListDto
    {
        public string Id { get; set; }
        public string Solicitud { get; set; }
        public string Nombre { get; set; }
        public string Registro { get; set; }
        public string Sucursal { get; set; }
        public string Edad { get; set; }
        public string Sexo { get; set; }
        public string Compañia { get; set; }
        public bool Seleccion { get; set; }
        public string ExpedienteId { get; set; }
        public string ClavePatologica { get; set; }
        public List<StudyDto> Estudios { get; set; }
    }
}
