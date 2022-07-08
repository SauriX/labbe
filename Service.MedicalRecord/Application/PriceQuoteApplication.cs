using ClosedXML.Excel;
using ClosedXML.Report;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Dtos.MedicalRecords;
using Service.MedicalRecord.Dtos.PriceQuote;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using Service.MedicalRecord.Utils;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application
{
    public class PriceQuoteApplication:IPriceQuoteApplication
    {
        public readonly  IPriceQuoteRepository _repository;
        private readonly ICatalogClient _catalogCliente;
        private readonly IPdfClient _pdfClient;
        public PriceQuoteApplication(IPriceQuoteRepository repository,ICatalogClient catalogClient, IPdfClient pdfClient)
        {
            _repository = repository;
            _catalogCliente = catalogClient;
            _pdfClient = pdfClient;
        }
        public async Task<List<PriceQuoteListDto>> GetNow(PriceQuoteSearchDto search)
        {
            var expedientes = await _repository.GetNow(search);

            return expedientes.ToPriceQuoteListDto();
        }
        public async Task<List<PriceQuoteListDto>> GetActive()
        {
            var expedientes = await _repository.GetActive();

            return expedientes.ToPriceQuoteListDto();
        }

        public async Task<PriceQuoteFormDto> GetById(Guid id)
        {
            var expediente = await _repository.GetById(id);

            return expediente.ToPriceQuoteFormDto();
        }
        public async Task<PriceQuoteListDto> Create(PriceQuoteFormDto priceQuote)
        {
            if (!string.IsNullOrEmpty(priceQuote.Id))
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }
            var date = DateTime.Now.ToString("ddMMyy");
            var newprice = priceQuote.ToModel();
            var codeRange = await _catalogCliente.GetCodeRange(priceQuote.SucursalId);
            var lastCode = await _repository.GetLastCode( date);

            var consecutive = Code.GetCode(codeRange, lastCode);
            var code = $"{consecutive}{date}";
            newprice.Afiliacion = code;
            await _repository.Create(newprice);
            newprice = await _repository.GetById(newprice.Id);

            return newprice.ToPriceQuoteListDto();
        }
        public async Task<PriceQuoteListDto> Update(PriceQuoteFormDto expediente)
        {
            // Helpers.ValidateGuid(parameter.Id, out Guid guid);

            var existing = await _repository.GetById(Guid.Parse(expediente.Id));

            /*if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }*/
            var date = DateTime.Now.ToString("ddMMyy");
            var updatedPack = expediente.ToModel(existing);
            var codeRange = await _catalogCliente.GetCodeRange(expediente.SucursalId);
            var lastCode = await _repository.GetLastCode(date);

            var consecutive = Code.GetCode(codeRange, lastCode);
            var code = $"{consecutive}{date}";
            updatedPack.Afiliacion = code;


            await _repository.Update(updatedPack);

            updatedPack = await _repository.GetById(updatedPack.Id);

            return updatedPack.ToPriceQuoteListDto();
        }
        public async Task<List<MedicalRecordsListDto>> GetMedicalRecord(PriceQuoteExpedienteSearch search) {
            var record = await _repository.GetMedicalRecord(search);
            return record.ToMedicalRecordsListDto();
        }
        public async Task<byte[]> GetTicket()
        {
            return await _pdfClient.GenerateTicket();
        }

        public async Task<(byte[] file, string fileName)> ExportList(PriceQuoteSearchDto search)
        {
            var studys = await GetNow(search);

            var path = Assets.CotizacionList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Cotizaciones");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Cotizaciones", studys);

            template.Generate();

            var range = template.Workbook.Worksheet("Cotizaciones").Range("Cotizaciones");
            var table = template.Workbook.Worksheet("Cotizaciones").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Cotizaciones.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportForm(Guid id)
        {
            var study = await GetById(id);

            var path = Assets.CotizacionForm;

            var template = new XLTemplate(path);
            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Cotizacion");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Cotizacion", study);

            template.Generate();

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Cotizacion ({study.nomprePaciente}).xlsx");
        }
    }
}
