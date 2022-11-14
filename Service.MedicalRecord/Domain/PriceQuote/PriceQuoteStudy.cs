using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Domain.Status;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Domain.PriceQuote
{
    public class PriceQuoteStudy
    {
        public Guid Id { get; set; }
        public Guid CotizacionId { get; set; }
        public virtual PriceQuote Cotizacion { get; set; }
        public int EstudioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int? PaqueteId { get; set; }
        public virtual RequestPack Paquete { get; set; }
        public Guid ListaPrecioId { get; set; }
        public string ListaPrecio { get; set; }
        public decimal Dias { get; set; }
        public int Horas { get; set; }
        public bool AplicaCargo { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioFinal { get; set; }
    }
}

