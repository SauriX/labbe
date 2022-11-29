using Service.MedicalRecord.Dtos.Request;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Quotation
{
    public class QuotationInfoDto
    {
        public Guid CotizacionId { get; set; }
        public string Clave { get; set; }
        public string Expediente { get; set; }
        public string Paciente { get; set; }
        public string Correo { get; set; }
        public string Whatsapp { get; set; }
        public string Fecha { get; set; }
        public bool Activo { get; set; }
        public IEnumerable<QuotationStudyInfoDto> Estudios { get; set; }
    }

    public class QuotationStudyInfoDto
    {
        public int Id { get; set; }
        public int EstudioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
    }
}