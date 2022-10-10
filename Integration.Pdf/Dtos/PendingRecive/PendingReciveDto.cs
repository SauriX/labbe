using System;
using System.Collections.Generic;

namespace Integration.Pdf.Dtos.PendingRecive
{
    public class PendingReciveDto
    {
        public string Id { get; set; }
        public string Nseguimiento { get; set; }
        public string Claveroute { get; set; }
        public string Sucursal { get; set; }
        public DateTime Fechaen { get; set; }
        public DateTime Horaen { get; set; }
        public DateTime Fechareal { get; set; }
        public List<ReciveStudyDto> Study { get; set; }
        public List<ExtraEstudyDto> Extra { get; set; }
        public StatusDto Status { get; set; }
    }
}
