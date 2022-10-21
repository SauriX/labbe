using Service.MedicalRecord.Dtos.MassSearch;
using System.Collections.Generic;
using System.Linq;

using Service.MedicalRecord.Domain.Request;

namespace Service.MedicalRecord.Mapper

{
    public static class MassSearchMapper
    {
        public static List<MassSearchInfoDto> ToMassSearchInfoDto(this List<Request> model)
        {
            return model.Select(x => new MassSearchInfoDto
            {

            }).ToList();
        }
    }
}
