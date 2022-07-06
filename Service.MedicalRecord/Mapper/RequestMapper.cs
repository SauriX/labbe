using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Request;

namespace Service.MedicalRecord.Mapper
{
    public static class RequestMapper
    {
        public static Request ToModel(this RequestDto dto)
        {
            return new Request();
        }
    }
}
