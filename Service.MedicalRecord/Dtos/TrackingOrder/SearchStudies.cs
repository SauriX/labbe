using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.TrackingOrder
{
    public class SearchStudies
    {
       public List<int> estudios { get; set; }
        public string solicitud { get; set; }
    }
}
