using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Domain;
using Service.Catalog.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class CatalogDescController : ControllerBase
    {
        //private readonly ICatalogDescApplication<Dimensions> _dimensionsService;
        private readonly ICatalogDescApplication<Indicators> _indicatorsService;
        private readonly ICatalogDescApplication<UseOfCFDI> _useOfCFDIService;
        private readonly ICatalogDescApplication<PaymentOption> _paymentOptionService;

        private readonly ICatalogDescApplication<Domain.Catalog.Service> _serviceService;

        public CatalogDescController(
            //ICatalogDescApplication<Dimensions> dimensionsService,
            ICatalogDescApplication<Indicators> indicatorsService,
            ICatalogDescApplication<UseOfCFDI> useOfCFDIService,
            ICatalogDescApplication<PaymentOption> paymentOptionService,

            ICatalogDescApplication<Domain.Catalog.Service> serviceService)
        {
            //_dimensionsService = dimensionsService;
            _indicatorsService = indicatorsService;
            _useOfCFDIService = useOfCFDIService;
            _paymentOptionService = paymentOptionService;
            _serviceService = serviceService;
        }
    }
}