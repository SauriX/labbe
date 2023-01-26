using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.PendingRecive
{
    public class PendingReciveDto
    {
        public string Id { get; set; }
        public string Nseguimiento { get; set; }
        public string Claveroute { get; set; }
        public string Solicitud { get; set; }
        public string Estudio { get; set; }
        public string Sucursal { get; set; }
        public DateTime Fechaen { get; set; }
        public DateTime Fechareal { get; set; }
        public StatusDto Status { get; set; }
    }
}
