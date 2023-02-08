﻿using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Quotation
{
    public class QuotationFilterDto
    {
        public DateTime? FechaAInicial { get; set; }
        public DateTime? FechaAFinal { get; set; }
        public List<string> Ciudad { get; set; }
        public List<Guid> Sucursales { get; set; }
        public string Correo{ get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaNInicial { get; set; }
        public DateTime? FechaNFinal { get; set; }
        public string Expediente { get; set; }
    }
}
