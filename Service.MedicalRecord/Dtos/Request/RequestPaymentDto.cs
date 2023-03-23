﻿using Newtonsoft.Json;
using System;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestPaymentDto
    {
        public Guid Id { get; set; }
        public Guid ExpedienteId { get; set; }
        public Guid SolicitudId { get; set; }
        public int FormaPagoId { get; set; }
        public string FormaPago { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal Cantidad { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public DateTime FechaPago { get; set; }
        public Guid FacturaId { get; set; }
        public string SerieFactura { get; set; }
        public string FacturapiId { get; set; }
        public string UsuarioRegistra { get; set; }
        public bool Lealtad { get; set; }
        public byte EstatusId { get; set; }
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}
