using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;

namespace Service.Report.Dtos.Indicators
{
    public class IndicatorsDto
    {
        public List<IndicatorsStatsDto> Indicadores { get; set; }
        public ServicesCostDto ServiciosTotal { get; set; }
    }
}
