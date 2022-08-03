﻿using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Domain.Request
{
    public class RequestPack : BaseModel
    {
        public Guid SolicitudId { get; set; }
        public virtual Request Solicitud { get; set; }
        public int PaqueteId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public Guid ListaPrecioId { get; set; }
        public string ListaPrecio { get; set; }
        public int? PromocionId { get; set; }
        public string Promocion { get; set; }
        public bool AplicaDescuento { get; set; }
        public bool AplicaCargo { get; set; }
        public bool AplicaCopago { get; set; }
        public decimal Dias { get; set; }
        public int Horas { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal PrecioFinal { get; set; }
        public virtual ICollection<RequestStudy> Estudios { get; set; }
    }
}
