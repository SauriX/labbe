﻿using System;

namespace Service.MedicalRecord.Dtos.MedicalRecords
{
    public class MedicalRecordsListDto
    {
        public Guid Id { get; set; }
        public string Expediente { get; set; }
        public string NomprePaciente { get; set; }
        public string Genero { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int MonederoElectronico { get; set; }
        public string Telefono { get; set; }
    }
}