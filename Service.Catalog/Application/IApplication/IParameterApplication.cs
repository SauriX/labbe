using Service.Catalog.Dtos.Parameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IParameterApplication
    {
        Task<IEnumerable<ParameterListDto>> GetAll(string search);
        Task<IEnumerable<ParameterListDto>> GetActive();
        Task<ParameterFormDto> GetById(string id);
        Task<IEnumerable<ParameterValueDto>> GetAllValues(string id);
        Task<ParameterValueDto> GetValueById(string id);
        Task<ParameterListDto> Create(ParameterFormDto parameter);
        Task AddValue(ParameterValueDto value);
        Task<ParameterListDto> Update(ParameterFormDto parameter);
        Task UpdateValue(ParameterValueDto value);
        Task DeleteValue(string id);
        Task<(byte[] file, string fileName)> ExportList(string search);
        Task<(byte[] file, string fileName)> ExportForm(string id);
    }
}
