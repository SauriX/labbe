using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class CatalogController : ControllerBase
    {
        private readonly ICatalogApplication<Delivery> _deliveryService;
        private readonly ICatalogApplication<Domain.Catalog.Service> _serviceService;

        public CatalogController(
            ICatalogApplication<Delivery> deliveryService,
            ICatalogApplication<Domain.Catalog.Service> serviceService)
        {
            _deliveryService = deliveryService;
            _serviceService = serviceService;
        }
    }
}
