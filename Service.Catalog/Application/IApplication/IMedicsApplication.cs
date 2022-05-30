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
        Task<byte[]> ExportList(string search = null);
        Task<byte[]> ExportForm(Guid id);
        //Task<string> GenerateCode(MedicsClaveDto medics, string suffix = null);

    }
}
