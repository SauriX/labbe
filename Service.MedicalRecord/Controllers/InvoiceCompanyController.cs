using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.InvoiceCompany;
using Shared.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceCompanyController : ControllerBase
    {
        private readonly IInvoiceCompanyApplication _service;
        public InvoiceCompanyController(IInvoiceCompanyApplication service)
        {
            _service = service;
        }

        [HttpPost("filter")]
        [Authorize(Policies.Access)]
        public async Task<InvoiceCompanyInfoDto> GetByFilter(InvoiceCompanyFilterDto filter)
        {
            return await _service.GetByFilter(filter);
        }

        [HttpGet("getConsecutiveBySerie/{serie}")]
        [Authorize(Policies.Access)]
        public async Task<string> GetConsecutiveBySerie(string serie)
        {
            return await _service.GetNextPaymentNumber(serie);
        }
    }
}
