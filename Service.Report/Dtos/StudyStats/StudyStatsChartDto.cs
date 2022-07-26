using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.StudyStats
{
    public class StudyStatsChartDto
    {
        public Guid Id { get; set; }
        public int CantidadPendiente { get; set; }
        public int CantidadTomaDeMuestra { get; set; }
        public int CantidadSolicitado { get; set; }
        public int CantidadCapturado { get; set; }
        public int CantidadValidado { get; set; }
        public int CantidadEnRuta { get; set; }
        public int CantidadLiberado { get; set; }
        public int CantidadEnviado { get; set; }
        public int CantidadEntregado { get; set; }
        public int CantidadCancelado { get; set; }
    }
}
