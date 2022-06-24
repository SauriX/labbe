using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Catalog.Dtos.Medicos;

namespace Identidad.Api.Infraestructure.Services.IServices
{
    public interface IMedicsApplication
    {
        Task<MedicsFormDto> GetById(Guid Id);
        Task<MedicsFormDto> Create(MedicsFormDto Medics);
        Task<MedicsFormDto> Update(MedicsFormDto medics);
        Task<IEnumerable<MedicsListDto>> GetAll(string search);
        Task<IEnumerable<MedicsListDto>> GetActive();
        Task<(byte[] file, string fileName)> ExportList(string search);
        Task<(byte[] file, string fileName)> ExportForm(Guid id);
        //Task<string> GenerateCode(MedicsClaveDto medics, string suffix = null);

    }
}
