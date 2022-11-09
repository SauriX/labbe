using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Dtos.WeeClinic
{
    public class WeeServicePricesDto
    {
        public WeeServicePricesTotalDto Total { get; set; }
        public WeeServicePricesSingleDto Paciente { get; set; }
        public WeeServicePricesSingleDto Aseguradora { get; set; }
    }

    public class WeeServicePricesTotalDto
    {
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public decimal RetencionIVA { get; set; }
        public decimal RetencionISR { get; set; }
        public decimal ImpuestoEstatal { get; set; }
        public decimal CopagoIsPorcentaje { get; set; }
        public decimal IEPS { get; set; }
        public decimal Descuento { get; set; }
        public decimal ISR { get; set; }
        public int IsCubierto { get; set; }
        public int? TipoIVA { get; set; }
        public int Cantidad { get; set; }
        public int TipoCopago { get; set; }
        public decimal? Copago { get; set; }
        public int? TipoDeducible { get; set; }
        public decimal Deducible { get; set; }
        public int? TipoCoaseguro { get; set; }
        public decimal Coaseguro { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal MontoExcedente { get; set; }
    }

    public class WeeServicePricesSingleDto
    {
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal SubTotalDescuento { get; set; }
        public decimal IVA { get; set; }
        public decimal RIVA { get; set; }
        public decimal RISR { get; set; }
        public decimal IEPS { get; set; }
        public decimal ImpuestoEstatal { get; set; }
        public decimal ISR { get; set; }
        public decimal TipoCosto { get; set; }
        public decimal IVAAplicado { get; set; }
        public decimal MontoExcedente { get; set; }
        public decimal Total { get; set; }
    }
}
