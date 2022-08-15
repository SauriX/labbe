using Service.Catalog.Dtos.Study;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IStudyApplication
    {
        Task<StudyFormDto> GetById(int Id);
        Task<IEnumerable<StudyListDto>> GetByIds(List<int> Id);
        Task<IEnumerable<StudyListDto>> GetAll(string search = null);
        Task<IEnumerable<StudyListDto>> GetActive();
        Task<StudyFormDto> Create(StudyFormDto study);
        Task<StudyFormDto> Update(StudyFormDto study);
        Task<(byte[] file, string fileName)> ExportList(string search = null);
        Task<(byte[] file, string fileName)> ExportForm(int id);
    }
}
