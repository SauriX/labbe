﻿using System;

namespace Service.Report.Domain.MedicalRecord
{
    public class MedicalRecord
    {
        public Guid Id { get; set; }
        public string Expediente { get; set; }
        public string Nombre { get; set; }
    }
}
