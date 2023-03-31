﻿using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Domain.Status;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Domain.Quotation
{
    public class QuotationStudy : BaseModel
    {
        public int Id { get; set; }
        public Guid CotizacionId { get; set; }
        public virtual Quotation Cotizacion { get; set; }
        public int EstudioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int? PaqueteId { get; set; }
        public virtual QuotationPack Paquete { get; set; }
        public Guid ListaPrecioId { get; set; }
        public string ListaPrecio { get; set; }
        public int? PromocionId { get; set; }
        public string Promocion { get; set; }
        public decimal Dias { get; set; }
        public int Horas { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal PrecioFinal { get; set; }
    }
}