using Service.Catalog.Domain.Parameter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IParameterRepository
    {
        Task<List<Parameters>> GetAll(string search);
        Task<Parameters> GetById(string id);
        Task Create(Parameters parameter);
        Task Update(Parameters parameter);
        Task<bool> ValidateClaveNamne(string clave, string nombre);
        Task addValuNumeric(TipoValor tipoValor);
        Task<TipoValor> getvalueNum(string id);
        Task updateValueNumeric(TipoValor tipoValor);
        Task<List<TipoValor>> Getvalues(string id,string tipe);
        Task<bool> existingvalue(string id);
        Task deletevalue(string id);

    }
}
