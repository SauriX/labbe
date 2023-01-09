using Service.MedicalRecord.PdfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.MedicalRecord.Dtos.DeliverOrder
{
    public class DeliverOrderdDto
    {
        public string Destino { get; set; }
        public string ResponsableRecive { get; set; }
        public string FechaEntestimada { get; set; }
        public string FechaEntreal { get; set; }
        public string Origen { get; set; }
        public string ResponsableEnvio { get; set; }
        public string TransportistqName { get; set; }
        public string Medioentrega { get; set; }
        public string FechaEnvio { get; set; }
        public List<Col> Columnas { get; set; }
        public List<Dictionary<string, object>> Datos { get; set; }
 


    }
}