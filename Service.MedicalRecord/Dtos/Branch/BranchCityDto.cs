using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Branch
{
    public class BranchCityDto
    {
        public IEnumerable<BranchInfoDto> Sucursales { get; set; }
        public string Ciudad { get; set; }
    }
}
