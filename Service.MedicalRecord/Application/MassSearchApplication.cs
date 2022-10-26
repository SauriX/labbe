using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.MassSearch;
using Service.MedicalRecord.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.MedicalRecord.Mapper;
using System;
using Service.MedicalRecord.Domain.Request;

namespace Service.MedicalRecord.Application
{
    public class MassSearchApplication : IMassSearchApplication
    {
        private readonly IMassSearchRepository _repository;

        public MassSearchApplication(IMassSearchRepository repository)
        {
            _repository = repository;
        }
        public async Task<MassSearchInfoDto> GetByFilter(MassSearchFilterDto filter)
        {
            var request = await _repository.GetByFilter(filter);

            return request.ToMassSearchInfoDto();
        }
    }
}
