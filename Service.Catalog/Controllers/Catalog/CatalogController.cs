using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Parameter;
using Service.Catalog.Domain.Provenance;

namespace Service.Catalog.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class CatalogController : ControllerBase
    {
        private readonly IAreaApplication _areaService;
        private readonly IBudgetApplication _budgetService;
        private readonly ICatalogApplication<Bank> _bankService;
        private readonly ICatalogApplication<Provenance> _provenanceService;
        private readonly ICatalogApplication<Clinic> _clinicService;
        private readonly ICatalogApplication<Department> _departmentService;
        private readonly IDimensionApplication _dimensionService;
        private readonly ICatalogApplication<Field> _fieldService;
        private readonly ICatalogDescriptionApplication<Payment> _paymentService;
        private readonly ICatalogDescriptionApplication<Indicator> _indicatorService;
        private readonly ICatalogApplication<WorkList> _workListService;
        private readonly ICatalogApplication<Delivery> _deliveryService;
        private readonly ICatalogApplication<Method> _methodService;
        private readonly ICatalogApplication<PaymentMethod> _paymentMethodService;
        private readonly ICatalogApplication<SampleType> _sampleTypeService;
        private readonly ICatalogDescriptionApplication<UseOfCFDI> _useOfCFDIService;
        private readonly ICatalogApplication<Format> _formatService;
        private readonly ICatalogApplication<Units> _unitService;
        private readonly ICatalogApplication<Equipos> _equipmentService;

        public CatalogController(
            ICatalogApplication<Delivery> deliveryService,
            IAreaApplication areaService,
            IBudgetApplication budgetService,
            ICatalogApplication<Bank> bankService,
            ICatalogApplication<Clinic> clinicService,
            ICatalogApplication<Department> departmentService,
            IDimensionApplication dimensionService,
            ICatalogApplication<Field> fieldService,
            ICatalogDescriptionApplication<Indicator> indicatorService,
            ICatalogApplication<Method> methodService,
            ICatalogApplication<SampleType> sampleTypeService,
            ICatalogDescriptionApplication<UseOfCFDI> useOfCFDIService,
            ICatalogDescriptionApplication<Payment> paymentService,
            ICatalogApplication<PaymentMethod> paymentMethodService,
            ICatalogApplication<WorkList> workListService,
            ICatalogApplication<Provenance> provenanceService,
            ICatalogApplication<Format> formatService,
            ICatalogApplication<Units> uniteService,
            ICatalogApplication<Equipos> equipoService
            )
        {
            _deliveryService = deliveryService;
            _areaService = areaService;
            _budgetService = budgetService;
            _bankService = bankService;
            _clinicService = clinicService;
            _departmentService = departmentService;
            _dimensionService = dimensionService;
            _fieldService = fieldService;
            _indicatorService = indicatorService;
            _methodService = methodService;
            _sampleTypeService = sampleTypeService;
            _useOfCFDIService = useOfCFDIService;
            _paymentService = paymentService;
            _paymentMethodService = paymentMethodService;
            _workListService = workListService;
            _provenanceService = provenanceService;
            _formatService = formatService;
            _unitService = uniteService;
            _equipmentService = equipoService;
        }
    }
}
