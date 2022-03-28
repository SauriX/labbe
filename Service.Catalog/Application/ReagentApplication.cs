using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Reagent;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using Service.Catalog.Transactions;
using Shared.Dictionary;
using Shared.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class ReagentApplication : IReagentApplication
    {
        private readonly IReagentRepository _repository;

        public ReagentApplication(IReagentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ReagentListDto>> GetAll(string search = null)
        {
            var reagents = await _repository.GetAll(search);

            return reagents.ToReagentListDto();
        }

        public async Task<ReagentFormDto> GetById(int id)
        {
            var reagent = await _repository.GetById(id);

            if (reagent == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return reagent.ToReagentFormDto();
        }

        public async Task Create(ReagentFormDto reagent)
        {
            if (reagent.Id != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newReagent = reagent.ToModel();

            await _repository.Create(newReagent);
        }

        public async Task Update(ReagentFormDto reagent)
        {
            var existing = await _repository.GetById(reagent.Id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = reagent.ToModel(existing);

            await _repository.Update(updatedAgent);
        }
    }
}
