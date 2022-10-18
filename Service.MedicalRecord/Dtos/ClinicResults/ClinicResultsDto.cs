using Service.MedicalRecord.Dtos.Sampling;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos
{
    public class ClinicResultsDto
    {
        public string Id { get; set; }
        public Guid ExpedienteId { get; set; }
        public string Solicitud { get; set; }
        public string Nombre { get; set; }
        public string Registro { get; set; }
        public string Entrega { get; set; }
        public string UsuarioCreo { get; set; }
        public string Sucursal { get; set; }
        public string SucursalNombre { get; set; }
        public string Edad { get; set; }
        public string Sexo { get; set; }
        public string Compañia { get; set; }
        public byte Procedencia { get; set; }
        public string Departamento { get; set; }
        public string Area { get; set; }
        public string NombreMedico { get; set; }
        public string ClavePatologica { get; set; }
        public List<StudyDto> Estudios { get; set; }
    }
}
