using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.InvoiceCompany;
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

        public async Task<IEnumerable<InvoiceCompanyInfoDto>> GetByFilter(InvoiceCompanyFilterDto filter)
        {
            //var request = await _repository.GetByFilter(filter);

            //return request.ToRequestInfoDto();
            throw new System.NotImplementedException();
        }
    }
}
