using System;
using System.Collections.Generic;

namespace Service.Report.Dtos
{
    public class ReportFilterDto
    {
        public List<Guid> SucursalId { get; set; }
        public List<Guid> MedicoId { get; set; }
        public List<Guid> CompañiaId { get; set; }
        public List<byte> MetodoEnvio { get; set; }
        public List<byte> Urgencia { get; set; }
        public List<byte> TipoCompañia { get; set; }
        public List<DateTime> Fecha { get; set; }
        public DateTime FechaIndividual { get; set; }
        public List<DateTime> Hora { get; set; }
        public bool Grafica { get; set; }
        public string User { get; set; } 

    }

    public class Reporte
    {
        public List<Dictionary<string, object>> GetData(ReportFilterDto filter)
        {
            string[] rows = { "PACIENTES", "INGRESS" };
            var data = new List<Dictionary<string, object>>();

            foreach (var item in rows)
            {
                var itemData = new Dictionary<string, object>
                {
                    ["Nombre"] = item
                };

                foreach (var s in filter.SucursalId)
                {
                    itemData.Add(s.ToString(), 300);
                }

               
                data.Add(itemData);
            }

            return data;
        }
    }
}