using ClosedXML.Excel;
using ClosedXML.Report;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Domain.MedicalRecord;
using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.MedicalRecords;
using Service.MedicalRecord.Dtos.Quotation;
using Service.MedicalRecord.Dtos.Reports;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using Service.MedicalRecord.Utils;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application
{
    public class MedicalRecordApplication : IMedicalRecordApplication
    {
        private readonly IMedicalRecordRepository _repository;
        private readonly ICatalogClient _catalogClient;
        private readonly IRepository<Branch> _branchRepository;

        public MedicalRecordApplication(
            IMedicalRecordRepository repository,
            ICatalogClient catalogClient,
            IRepository<Branch> branchRepository)
        {
            _repository = repository;
            _catalogClient = catalogClient;
            _branchRepository = branchRepository;
        }

        public async Task<List<MedicalRecordsListDto>> GetAll()
        {
            var expedientes = await _repository.GetAll();

            return expedientes.ToMedicalRecordsListDto();
        }

        public async Task<List<MedicalRecordsListDto>> GetNow(MedicalRecordSearch search)
        {
            var expedientes = await _repository.GetNow(search);
            var expedientesListDto = expedientes.AsQueryable();

            var sucursales = await _catalogClient.GetBranchbycity();

            if (search.ciudad != null)
            {
                if (search.ciudad.Length > 0)
                {
                    var ciudad = sucursales.FindAll(x => search.ciudad.Contains(x.Ciudad));

                    expedientesListDto = expedientesListDto.Where(x => ciudad.Any(y => y.Sucursales.Any(z => Guid.Parse(z.IdSucursal) == x.IdSucursal)));
                }
            }

            expedientes = expedientesListDto.ToList();

            var data = expedientes.ToMedicalRecordsListDto();

            foreach (var expediente in data)
            {
                var branch = await _branchRepository.GetOne(x => x.Id == expediente.SucursalId);
                if (branch != null)
                {
                    expediente.Sucursal = branch.Nombre;
                }
            }

            return data;
        }

        public async Task<List<MedicalRecordDto>> GetMedicalRecord(List<Guid> records)
        {
            var expedientes = await _repository.GetRecordsByIds(records);

            return expedientes.ToMedicalRecordDto();
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
            var date = DateTime.Now.ToString("yyMMdd");

            var branch = await _branchRepository.GetOne(x => x.Id == newprice.IdSucursal);
            var lastCode = await _repository.GetLastCode(newprice.IdSucursal, date);

            var code = Codes.GetCode(branch.Codigo, lastCode);

            newprice.Expediente = code;
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
        public async Task<bool> UpdateWallet(ExpedienteMonederoDto monedero)
        {
            var existing = await _repository.GetById(monedero.Id);

            existing.MonederoActivo = monedero.Activo;

            existing.Monedero = monedero.Saldo;

            existing.FechaActivacionMonedero = DateTime.Now;

            await _repository.UpdateWallet(existing);

            return true;

        }
        public async Task UpdateTaxData(TaxDataDto taxData)
        {
            if (taxData.ExpedienteId == null || taxData.ExpedienteId == Guid.Empty || taxData.Id == Guid.Empty)
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
            var studies = await GetNow(search);

            var path = Assets.ExpedientetList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Expedientes");
            if (search.fechaAlta != null)
            {
                template.AddVariable("FechaInicial", search.fechaAlta[0].ToString("dd/MM/yyyy"));
                template.AddVariable("FechaFinal", search.fechaAlta[1].ToString("dd/MM/yyyy"));
            }
            else
            {
                template.AddVariable("FechaInicial", DateTime.MinValue.ToShortDateString());
                template.AddVariable("FechaFinal", DateTime.Now.ToShortDateString());
            }
            template.AddVariable("Expedientes", studies);

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
