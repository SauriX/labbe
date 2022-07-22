﻿using System;

namespace Service.Report.Domain.MedicalRecord
{
    public class MedicalRecord : Base
    {
        public Guid Id { get; set; }
        public string Expediente { get; set; }
        public string Nombre { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
    }
}
