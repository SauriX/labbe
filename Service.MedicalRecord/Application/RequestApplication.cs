using Service.Catalog.Transactions;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using Service.MedicalRecord.Utils;
using Shared.Dictionary;
using Shared.Error;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application
{
    public class RequestApplication : IRequestApplication
    {
        private readonly IRequestRepository _repository;
        private readonly ICatalogClient _catalogClient;
        private readonly IPdfClient _pdfClient;
        private readonly ITransactionProvider _transaction;

        public RequestApplication(
            IRequestRepository repository,
            ICatalogClient catalogClient,
            ITransactionProvider transaction,
            IPdfClient pdfClient)
        {
            _repository = repository;
            _catalogClient = catalogClient;
            _transaction = transaction;
            _pdfClient = pdfClient;
        }

        public async Task<byte[]> GetTicket()
        {
            return await _pdfClient.GenerateTicket();
        }

        public async Task<string> Create(RequestDto request)
        {
            if (request.Id == null || request.Id == Guid.Empty)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            _transaction.BeginTransaction();

            try
            {
                var date = DateTime.Now.ToString("ddMMyy");

                var codeRange = await _catalogClient.GetCodeRange(request.SucursalId);
                var lastCode = await _repository.GetLastCode(request.SucursalId, date);

                var consecutive = Code.GetCode(codeRange, lastCode);
                var code = $"{consecutive}{date}";

                var newRequest = request.ToModel();

                //await _repository.Create(newRequest);

                //if (request.General != null)
                //{

                //}

                //var newReagent = reagent.ToModel();

                //await CheckDuplicate(newReagent);

                //await _repository.Create(newReagent);

                _transaction.CommitTransaction();

                return "";
            }
            catch (Exception)
            {
                _transaction.RollbackTransaction();
                throw;
            }
        }
    }
}
