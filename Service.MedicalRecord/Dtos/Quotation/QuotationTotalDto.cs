﻿using System;
using System.Text.Json.Serialization;

namespace Service.MedicalRecord.Dtos.Quotation
{
    public class QuotationTotalDto
    {
        public Guid CotizacionId { get; set; }
        public decimal TotalEstudios { get; set; }
        public decimal Cargo { get; set; }
        public byte CargoTipo { get; set; }
        public decimal Total { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}
