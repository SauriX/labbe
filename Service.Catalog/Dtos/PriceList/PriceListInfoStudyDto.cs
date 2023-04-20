using Service.Catalog.Dtos.Indication;
using Service.Catalog.Dtos.Parameter;
using Service.Catalog.Dtos.Study;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.PriceList
{
    public class PriceListInfoStudyDto
    {
        public string Identificador { get; set; }
        public Guid ListaPrecioId { get; set; }
        public string ListaPrecio { get; set; }
        public int? PromocionId { get; set; }
        public string Promocion { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public int EstudioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int? DepartamentoId { get; set; }
        public int? AreaId { get; set; }
        public int Dias { get; set; }
        public int Horas { get; set; }
        public int Orden { get; set; }
        public string Destino { get; set; }
        public Guid? DestinoId { get; set; }
        public byte DestinoTipo { get; set; }
        public int? MaquilaId { get; set; }
        public string Maquila { get; set; }
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
        public IEnumerable<ParameterListDto> Parametros { get; set; }
        public IEnumerable<IndicationListDto> Indicaciones { get; set; }
        public IEnumerable<StudyTagDto> Etiquetas { get; set; }
    }

    public class PriceListInfoPromoDto
    {
        public int EstudioId { get; set; }
        public int PaqueteId { get; set; }
        public int? PromocionId { get; set; }
        public string Promocion { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
    }
}
