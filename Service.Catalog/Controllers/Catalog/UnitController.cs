﻿using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers.Catalog
{
    public partial class CatalogController : ControllerBase
    {
        [HttpGet("units/active")]
        public async Task<IEnumerable<CatalogListDto>> GetActiveUnits()
        {
            return await _unitService.GetActive();
        }
    }
}
