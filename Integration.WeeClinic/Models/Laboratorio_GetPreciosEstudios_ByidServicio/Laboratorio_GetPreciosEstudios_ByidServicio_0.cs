using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Integration.WeeClinic.Models.Laboratorio_GetPreciosEstudios_ByidServicio
{
    public class Laboratorio_GetPreciosEstudios_ByidServicio_0
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
        [JsonPropertyName("isCubierto")]
        public int IsCubierto { get; set; }
        public int TipoIVA { get; set; }
        public int Cantidad { get; set; }
        public int TipoCopago { get; set; }
        public decimal Copago { get; set; }
        public int? TipoDeducible { get; set; }
        public decimal Deducible { get; set; }
        public int? TipoCoaseguro { get; set; }
        public decimal Coaseguro { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal MontoExcedente { get; set; }

        //
        public string Mensaje { get; set; }

        //
        [JsonPropertyName("isAvaliable")]
        public int? IsAvaliable { get; set; }
        public int? RestanteDays { get; set; }
        public int? Vigencia { get; set; }
    }
}
