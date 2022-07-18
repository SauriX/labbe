using Service.MedicalRecord.Dtos.MedicalRecords;
using Service.MedicalRecord.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.MedicalRecord.Mapper;
using System;
using Shared.Error;
using System.Net;
using Shared.Dictionary;
using Service.MedicalRecord.Application.IApplication;
using ClosedXML.Report;
using ClosedXML.Excel;
using Shared.Extensions;
using Service.MedicalRecord.Dtos;
using Shared.Helpers;
using Service.MedicalRecord.Domain.MedicalRecord;
using Service.MedicalRecord.Dictionary;

namespace Service.MedicalRecord.Application
{
    public class MedicalRecordApplication : IMedicalRecordApplication
    {
        public readonly IMedicalRecordRepository _repository;

        public MedicalRecordApplication(IMedicalRecordRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MedicalRecordsListDto>> GetAll()
        {
            var expedientes = await _repository.GetAll();

            return expedientes.ToMedicalRecordsListDto();
        }

        public async Task<List<MedicalRecordsListDto>> GetNow(MedicalRecordSearch search)
        {
            var expedientes = await _repository.GetNow(search);

            return expedientes.ToMedicalRecordsListDto();
        }

        public async Task<List<MedicalRecordsListDto>> GetActive()
        {
            var expedientes = await _repository.GetActive();

            return expedientes.ToMedicalRecordsListDto();
        }

        public async Task<List<TaxDataDto>> GetTaxData(Guid recordId)
        {
            var taxData = await _repository.GetTaxData(recordId);

            return taxData.ToTaxDataDto();
        }

        public async Task<MedicalRecordsFormDto> GetById(Guid id)
        {
            var expediente = await _repository.GetById(id);

            return expediente.ToMedicalRecordsFormDto();
        }

        public async Task<MedicalRecordsListDto> Create(MedicalRecordsFormDto expediente)
        {
            if (!string.IsNullOrEmpty(expediente.Id))
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newprice = expediente.ToModel();

            await _repository.Create(newprice, expediente.TaxData);

            newprice = await _repository.GetById(newprice.Id);

            return newprice.ToMedicalRecordsListDto();
        }

        public async Task<string> CreateTaxData(TaxDataDto taxData)
        {
            if (taxData.ExpedienteId == null || taxData.ExpedienteId == Guid.Empty || (taxData.Id != null && taxData.Id != Guid.Empty))
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newTaxData = taxData.ToTaxData();

            var recordTaxData = new MedicalRecordTaxData(newTaxData.Id, (Guid)taxData.ExpedienteId, taxData.UsuarioId);

            await _repository.CreateTaxData(newTaxData, recordTaxData);

            return newTaxData.Id.ToString();
        }

        public async Task<List<MedicalRecordsListDto>> Coincidencias(MedicalRecordsFormDto expediente)
        {
            var coincidencias = await _repository.Coincidencias(expediente.ToModel());

            return coincidencias.ToMedicalRecordsListDto();
        }

        public async Task<MedicalRecordsListDto> Update(MedicalRecordsFormDto expediente)
        {
            // Helpers.ValidateGuid(parameter.Id, out Guid guid);

            var existing = await _repository.GetById(Guid.Parse(expediente.Id));

            /*if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }*/

            var updatedPack = expediente.ToModel(existing);

            await _repository.Update(updatedPack, expediente.TaxData);

            updatedPack = await _repository.GetById(updatedPack.Id);

            return updatedPack.ToMedicalRecordsListDto();
        }

        public async Task UpdateTaxData(TaxDataDto taxData)
        {
            if (taxData.ExpedienteId == null || taxData.ExpedienteId == Guid.Empty || taxData.Id == null || taxData.Id == Guid.Empty)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var existing = await _repository.GetTaxDataById((Guid)taxData.Id, (Guid)taxData.ExpedienteId);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedTaxData = taxData.ToTaxDataUpdate(existing);

            await _repository.UpdateTaxData(updatedTaxData);
        }

        public async Task<(byte[] file, string fileName)> ExportList(MedicalRecordSearch search)
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

            return (template.ToByteArray(), $"Catálogo de Expedientes ({study.Expediente}).xlsx");
        }
    }
}
