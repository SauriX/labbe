﻿using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IAreaApplication
    {
        Task<IEnumerable<AreaListDto>> GetAll(string search);
        Task<IEnumerable<AreaListDto>> GetActive();
        Task<IEnumerable<AreaListDto>> GetAreaByDepartment(int departmentId);
        Task<AreaFormDto> GetById(int id);
        Task<AreaListDto> Create(AreaFormDto Catalog);
        Task<AreaListDto> Update(AreaFormDto Catalog);
        Task<byte[]> ExportList(string search);
        Task<byte[]> ExportForm(int id);
    }
}
