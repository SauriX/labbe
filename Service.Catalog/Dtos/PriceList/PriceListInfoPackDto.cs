using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Dtos.PriceList
{
    public class PriceListInfoPackDto
    {
        public string Identificador { get; set; }
        public Guid ListaPrecioId { get; set; }
        public string ListaPrecio { get; set; }
        public int? PromocionId { get; set; }
        public string Promocion { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public int PaqueteId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int DepartamentoId { get; set; }
        public int AreaId { get; set; }
        public int Dias => Estudios.Max(x => x.Dias);
        public int Horas => Estudios.Max(x => x.Horas);
        public decimal PrecioEstudios { get; set; }
        public decimal PaqueteDescuento { get; set; }
        public decimal PaqueteDescuentoProcentaje { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioFinal => Precio - Descuento;
        public DateTime FechaEntrega
        {
            get
            {
                var datePlusHours = DateTime.Now.AddHours(Horas);

                if (datePlusHours.DayOfWeek == DayOfWeek.Sunday || (datePlusHours.DayOfWeek == DayOfWeek.Saturday && datePlusHours.Hour > 17))
                {
                    datePlusHours = datePlusHours.DayOfWeek == DayOfWeek.Sunday ? datePlusHours.AddDays(1) : datePlusHours.AddDays(2);
                }
                if (datePlusHours.Hour > 17)
                {
                    datePlusHours = datePlusHours.AddDays(1);
                }

                return new DateTime(datePlusHours.Year, datePlusHours.Month, datePlusHours.Day, 17, 0, 0);
            }
        }
        public List<PriceListInfoPromoDto> Promociones { get; set; }
        public List<PriceListInfoStudyDto> Estudios { get; set; }
    }
}
