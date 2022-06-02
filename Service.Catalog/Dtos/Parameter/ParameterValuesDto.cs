using Service.Catalog.Dtos.Parameters;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Parameter
{
    public class ParameterValuesDto
    {
        public List<ParameterValueDto> Values { get; set; }
        public string IdParameter { get; set; }
    }
}
