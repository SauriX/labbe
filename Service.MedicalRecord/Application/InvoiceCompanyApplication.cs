using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.InvoiceCompany;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application
{
    public class InvoiceCompanyApplication : IInvoiceCompanyApplication
    {
        private readonly IRequestRepository _repository;

        public InvoiceCompanyApplication(IRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<InvoiceCompanyInfoDto> GetByFilter(InvoiceCompanyFilterDto filter)
        {
            var request = await _repository.InvoiceCompanyFilter(filter);

            return request.ToInvoiceCompanyDto();
        }
    }
}
