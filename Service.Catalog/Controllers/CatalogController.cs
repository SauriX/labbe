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
    public partial class CatalogController : ControllerBase
    {
        private readonly ICatalogApplication<Delivery> _deliveryService;
        private readonly ICatalogApplication<Area> _areaService;
        private readonly ICatalogApplication<Bank> _bankService;
        private readonly ICatalogApplication<Clinic> _clinicService;
        private readonly ICatalogApplication<Departments> _departmentsService;
        private readonly ICatalogApplication<Dimensions> _dimensionsService;
        private readonly ICatalogApplication<Specialty> _specialtyService;
        private readonly ICatalogApplication<Indicators> _indicatorsService;
        private readonly ICatalogApplication<Packages> _packagesService;
        private readonly ICatalogApplication<Methods> _methodsService;
        private readonly ICatalogApplication<SampleType> _sampleTypeService;
        private readonly ICatalogApplication<UseOfCFDI> _useOfCFDIService;
        private readonly ICatalogApplication<PaymentOption> _paymentOptionService;
        private readonly ICatalogApplication<PaymentMethod> _paymentMethodService;
        private readonly ICatalogApplication<WorkLists> _workListsService;

        private readonly ICatalogApplication<Domain.Catalog.Service> _serviceService;

        public CatalogController(
            ICatalogApplication<Delivery> deliveryService,
            ICatalogApplication<Area> areaService,
            ICatalogApplication<Bank> bankService,
            ICatalogApplication<Clinic> clinicService,
            ICatalogApplication<Departments> departmentsService,
            ICatalogApplication<Dimensions> dimensionsService,
            ICatalogApplication<Specialty> specialtyService,
            ICatalogApplication<Indicators> indicatorsService,
            ICatalogApplication<Packages> packagesService,
            ICatalogApplication<Methods> methodsService,
            ICatalogApplication<SampleType> sampleTypeService,
            ICatalogApplication<UseOfCFDI> useOfCFDIService,
            ICatalogApplication<PaymentOption> paymentOptionService,
            ICatalogApplication<PaymentMethod> paymentMethodService,
            ICatalogApplication<WorkLists> workListsService,

            ICatalogApplication<Domain.Catalog.Service> serviceService)
        {
            _deliveryService = deliveryService;
            _areaService = areaService;  
            _bankService = bankService; 
            _clinicService = clinicService; 
            _departmentsService = departmentsService;
            _dimensionsService = dimensionsService;
            _specialtyService = specialtyService;
            _indicatorsService = indicatorsService;
            _packagesService = packagesService;
            _methodsService = methodsService;
            _sampleTypeService = sampleTypeService;
            _useOfCFDIService = useOfCFDIService;
            _paymentOptionService = paymentOptionService;
            _paymentMethodService = paymentMethodService;
            _workListsService = workListsService;
            _serviceService = serviceService;
        }
    }
}
