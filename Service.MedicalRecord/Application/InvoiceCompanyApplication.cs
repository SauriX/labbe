using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.InvoiceCompany;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using System;
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
        public async Task<string> GetNextPaymentNumber(string serie)
        {
            var date = DateTime.Now.ToString("yy");

            var lastCode = await _repository.GetLastPaymentCode(serie, date);
            var consecutive = lastCode == null ? 1 : Convert.ToInt32(lastCode.Replace(date, "")) + 1;

            return $"{date}{consecutive:D5}";
        }
       
    }
}
