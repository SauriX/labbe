using Service.Catalog.Dtos.Parameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IParameterApplication
    {
        Task<IEnumerable<ParameterList>> GetAll(string search = null);
        Task<ParameterForm> GetById(string id);
        Task Create(ParameterForm parameter);
        Task Update(ParameterForm parameter);
        Task<byte[]> ExportList(string search = null);
        Task<byte[]> ExportForm(string id);
        Task AddValue(ValorTipeForm valorTipeForm);
        Task<ValorTipeForm> getvalueNum(string id);
        Task updateValueNumeric(ValorTipeForm tipoValor);
        Task<IEnumerable<ValorTipeForm>> getallvalues(string id,string tipe);
        Task deletevalue(string id);
    }
}
