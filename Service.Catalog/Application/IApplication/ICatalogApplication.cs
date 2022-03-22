﻿using Service.Catalog.Domain.Catalog;
using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface ICatalogApplication<T> where T : GenericCatalog
    {
        Task<IEnumerable<CatalogListDto>> GetAll(string search = null);
        Task<CatalogFormDto> GetById(int id);
        Task Create(CatalogFormDto Catalog);
        Task Update(CatalogFormDto Catalog);
    }
}
