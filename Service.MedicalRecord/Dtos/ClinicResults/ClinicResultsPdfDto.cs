﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Dtos.ClinicResults
{
    public class ClinicResultsPdfDto
    {
        public List<ClinicResultsFormDto> CapturaResultados { get; set; }
        public ClinicResultsRequestDto SolicitudInfo { get; set; }
        public bool ImprimrLogos { get; set; }
        public bool ImprimirCriticos { get; set; }
        public bool ImprimirPrevios { get; set; }
    }
}
