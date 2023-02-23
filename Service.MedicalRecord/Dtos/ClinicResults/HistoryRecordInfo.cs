using System;

namespace Service.MedicalRecord.Dtos.ClinicResults
{
    public class HistoryRecordInfo
    {
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
        public Guid SolicitudId { get; set; }
    }
}
