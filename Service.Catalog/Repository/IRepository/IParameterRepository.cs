using Service.Catalog.Domain.Parameter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IParameterRepository
    {
        Task<List<Parameter>> GetAll(string search);
        Task<List<Parameter>> GetActive();
        Task<Parameter> GetById(Guid id);
        Task<List<ParameterValue>> GetAllValues(Guid id);
        Task<ParameterValue> GetValueById(Guid id);
        Task<bool> IsDuplicate(Parameter reagent);
        Task Create(Parameter parameter);
        Task AddValue(ParameterValue value);
        Task Update(Parameter parameter);
        Task UpdateValue(ParameterValue value);
        Task DeleteValue(Guid id);
    }
}
