using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Dtos.Catalogs
{
    public class ParameterValueDto
    {
        public string Id { get; set; }
        public string ParametroId { get; set; }
        public string Nombre { get; set; }
        public decimal ValorInicial { get; set; }
        public decimal ValorFinal { get; set; }
        public decimal ValorInicialNumerico { get; set; }
        public decimal ValorFinalNumerico { get; set; }
        public int RangoEdadInicial { get; set; }
        public int RangoEdadFinal { get; set; }
        public decimal HombreValorInicial { get; set; }
        public decimal HombreValorFinal { get; set; }
        public decimal MujerValorInicial { get; set; }
        public decimal MujerValorFinal { get; set; }
        public decimal CriticoMinimo { get; set; }
        public decimal CriticoMaximo { get; set; }
        public decimal HombreCriticoMinimo { get; set; }
        public decimal HombreCriticoMaximo { get; set; }
        public decimal MujerCriticoMinimo { get; set; }
        public decimal MujerCriticoMaximo { get; set; }
        public byte MedidaTiempoId { get; set; }
        public string Opcion { get; set; }
        public string DescripcionTexto { get; set; }
        public string DescripcionParrafo { get; set; }
    }
}
