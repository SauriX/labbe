using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Dtos.DeliverOrder
{
    public class DeliverOrderStudyDto
    {
        public string Clave { get; set; }
        public string Estudio { get; set; }
        public decimal Temperatura { get; set; }
        public string Paciente { get; set; }
        public bool ConfirmacionOrigen { get; set; }
        public bool ConfirmacionDestino { get; set; }

    }
}