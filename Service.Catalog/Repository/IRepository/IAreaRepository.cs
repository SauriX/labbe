using Service.Catalog.Domain.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IAreaRepository
    {
        Task<List<Area>> GetAll(string search);
        Task<List<Area>> GetActive();
        Task<List<Area>> GetAreaByDepartment(int departmentId);
        Task<List<Area>> GetAreasByDepartments();
        Task<Area> GetById(int id);
        Task<bool> IsDuplicate(Area area);
        Task Create(Area area);
        Task Update(Area area);
        Task<IEnumerable<Area>> GetAreas(int id);

    }
}
