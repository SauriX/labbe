﻿using Service.Catalog.Dtos.Branch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IBranchApplication
    {
        Task<(byte[] file, string fileName)> ExportListBranch(string search = null);
        Task<(byte[] file, string fileName)> ExportFormBranch(string id);
        Task<IEnumerable<BranchInfoDto>> GetAll(string search = null);
        Task<BranchFormDto> GetById(string Id);
        Task<BranchFormDto> GetByName(string name);
        Task<string> GetCodeRange(Guid id);
        Task<bool> Create(BranchFormDto branch);
        Task<bool> Update(BranchFormDto branch);
        Task<IEnumerable<BranchCityDto>> GetBranchByCity();
    }
}
