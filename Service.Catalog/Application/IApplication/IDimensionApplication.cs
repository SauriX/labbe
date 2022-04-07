﻿using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IDimensionApplication
    {
        Task<IEnumerable<DimensionListDto>> GetAll(string search = null);
        Task<DimensionFormDto> GetById(int id);
        Task<DimensionListDto> Create(DimensionFormDto Catalog);
        Task<DimensionListDto> Update(DimensionFormDto Catalog);
    }
}
