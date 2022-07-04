using System.Collections.Generic;

namespace Service.Catalog.Dtos.Branch
{
    public class BranchCityDto
    {
        public IEnumerable<BranchInfoDto> Sucursales { get; set; }
        public string Ciudad { get; set; }
    }
}
