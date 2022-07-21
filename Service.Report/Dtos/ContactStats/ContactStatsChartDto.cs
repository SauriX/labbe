using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.ContactStats
{
    public class ContactStatsChartDto
    {
        public int Cant_Celular { get; set; }
        public int Cant_Correo { get; set; }
        public int Total => Cant_Celular + Cant_Correo;
    }
}
