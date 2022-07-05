using ClosedXML.Excel;
using ClosedXML.Report;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Dtos.MedicalRecords;
using Service.MedicalRecord.Dtos.PriceQuote;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
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
        public PriceQuoteApplication(IPriceQuoteRepository repository)
        {
            _repository = repository;
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

            var newprice = priceQuote.ToModel();


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

            var updatedPack = expediente.ToModel(existing);



            await _repository.Update(updatedPack);

            updatedPack = await _repository.GetById(updatedPack.Id);

            return updatedPack.ToPriceQuoteListDto();
        }
        public async Task<List<MedicalRecordsListDto>> GetMedicalRecord(PriceQuoteExpedienteSearch search) {
            var record = await _repository.GetMedicalRecord(search);
            return record.ToMedicalRecordsListDto();
        }
        public async Task<(byte[] file, string fileName)> ExportList(PriceQuoteSearchDto search)
        {
            var studys = await GetNow(search);

            var path = Assets.ExpedientetList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Expedientes");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Expedientes", studys);

            template.Generate();

            var range = template.Workbook.Worksheet("Expedientes").Range("Expedientes");
            var table = template.Workbook.Worksheet("Expedientes").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Expedientes.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportForm(Guid id)
        {
            var study = await GetById(id);

            var path = Assets.ExpedienteForm;

            var template = new XLTemplate(path);
            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Expediente");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Expediente", study);

            template.Generate();

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Expedientes ({study.nomprePaciente}).xlsx");
        }
    }
}
