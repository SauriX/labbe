using Service.MedicalRecord.Dtos.Catalogs;
using Service.MedicalRecord.Dtos.Promotion;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestStudyDto
    {
        private DateTime fechaEntrega;
        public int Id { get; set; }
        public string Type => "study";
        public Guid SolicitudId { get; set; }
        public int EstudioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int? PaqueteId { get; set; }
        public string Paquete { get; set; }
        public Guid ListaPrecioId { get; set; }
        public string ListaPrecio { get; set; }
        public int? PromocionId { get; set; }
        public string Promocion { get; set; }
        public int TaponId { get; set; }
        public string TaponColor { get; set; }
        public string TaponClave { get; set; }
        public string TaponNombre { get; set; }
        public int DepartamentoId { get; set; }
        public int AreaId { get; set; }
        public byte EstatusId { get; set; }
        public string Estatus { get; set; }
        public bool AplicaDescuento { get; set; }
        public bool AplicaCargo { get; set; }
        public bool AplicaCopago { get; set; }
        public decimal Dias { get; set; }
        public int Horas { get; set; }
        public DateTime FechaEntrega { get => fechaEntrega == DateTime.MinValue ? DateTime.Now : fechaEntrega; set => fechaEntrega = value; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal PrecioFinal { get; set; }
        public string NombreEstatus { get; set; }
        public string FechaActualizacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public List<PriceListInfoPromoDto> Promociones { get; set; }
        public List<ParameterListDto> Parametros { get; set; }
        public List<IndicationListDto> Indicaciones { get; set; }
    }
}
