﻿using System;
using System.Collections.Generic;
using Service.MedicalRecord.Dtos.Catalogs;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.Sampling;

namespace Service.MedicalRecord.Dtos.ClinicResults
{
    public class ClinicResultsCaptureDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public Guid SolicitudId { get; set; }
        public int EstudioId { get; set; }
        public string ParametroId { get; set; }
        public int TipoValor { get; set; }
        public int ValorInicial { get; set; }
        public int ValorFinal { get; set; }
        public string Resultado { get; set; }
    }
}
