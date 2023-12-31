﻿using Service.Catalog.Domain.Catalog;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface ICatalogApplication<T> where T : GenericCatalog
    {
        Task<IEnumerable<CatalogListDto>> GetAll(string search);
        Task<IEnumerable<CatalogListDto>> GetActive();
        Task<CatalogFormDto> GetById(int id);
        Task<CatalogListDto> Create(CatalogFormDto Catalog);
        Task<CatalogListDto> Update(CatalogFormDto Catalog);
        Task<byte[]> ExportList(string search, string catalogName);
        Task<(byte[] file, string code)> ExportForm(int id, string catalogName);
    }
}
