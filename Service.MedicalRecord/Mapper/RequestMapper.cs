using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Request;
using System.Collections.Generic;

namespace Service.MedicalRecord.Mapper
{
    public static class RequestMapper
    {
        public static Request ToModel(this RequestDto dto)
        {
            return new Request();
        }

        public static IEnumerable<RequestStudy> ToModel(this IEnumerable<RequestStudyDto> dto)
        {
            return new List<RequestStudy>();
        }
    }
}
