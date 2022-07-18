using Microsoft.Extensions.Configuration;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using Service.MedicalRecord.Transactions;
using Service.MedicalRecord.Utils;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application
{
    public class RequestApplication : IRequestApplication
    {
        private readonly string _imageUrl;
        private readonly IRequestRepository _repository;
        private readonly ICatalogClient _catalogClient;
        private readonly IPdfClient _pdfClient;
        private readonly ITransactionProvider _transaction;

        public RequestApplication(
            IConfiguration configuration,
            IRequestRepository repository,
            ICatalogClient catalogClient,
            ITransactionProvider transaction,
            IPdfClient pdfClient)
        {
            _imageUrl = configuration.GetValue<string>("Request:ImageUrl");
            _repository = repository;
            _catalogClient = catalogClient;
            _transaction = transaction;
            _pdfClient = pdfClient;
        }

        public async Task<byte[]> GetTicket()
        {
            return await _pdfClient.GenerateTicket();
        }

        public async Task<byte[]> GetOrder()
        {
            return await _pdfClient.GenerateOrder();
        }

        public async Task<string> Create(RequestDto request)
        {
            _transaction.BeginTransaction();

            try
            {
                var date = DateTime.Now.ToString("ddMMyy");

                var codeRange = await _catalogClient.GetCodeRange(request.SucursalId);
                var lastCode = await _repository.GetLastCode(request.SucursalId, date);

                var consecutive = Code.GetCode(codeRange, lastCode);
                var code = $"{consecutive}{date}";

                var newRequest = request.ToModel();
                newRequest.Clave = code;

                var pricesAreValid = true;

                if (!pricesAreValid)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, "Los precios seleccionados no coinciden con los precios asignados de los estudios");
                }

                await _repository.Create(newRequest);

                _transaction.CommitTransaction();

                return newRequest.Id.ToString();
            }
            catch (Exception)
            {
                _transaction.RollbackTransaction();

                throw;
            }
        }

        public async Task SaveImage(RequestImageDto requestImage)
        {
            var request = await _repository.GetById(requestImage.SolicitudId);

            if (request == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var typeOk = requestImage.Tipo.In("orden", "ine", "formato");

            var isImage = requestImage.Imagen.IsImage();

            if (!typeOk || !isImage)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Tipo de imagén no válida");
            }

            requestImage.Clave = request.Clave;
            var path = await SaveImageGetPath(requestImage);

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Hubo un error al guardar la imagén, por favor vuelva a intentarlo");
            }

            if (requestImage.Tipo == "orden")
            {
                request.RutaOrden = path;
            }
            else if (requestImage.Tipo == "ine")
            {
                request.RutaINE = path;
            }
            else
            {
                request.RutaFormato = path;
            }

            await _repository.Update(request);
        }

        private async Task<string> SaveImageGetPath(RequestImageDto request)
        {
            var path = Path.Combine(_imageUrl, "Solicitudes", request.Clave);
            var name = string.Concat(request.Tipo, ".png");

            var isSaved = await request.Imagen.SaveFileAsync(path, name);

            if (isSaved)
            {
                return Path.Combine(path, name);
            }

            return null;
        }

        public async Task<bool> SendTestEmail()
        {
            return await Task.FromResult(true);
        }
    }
}
