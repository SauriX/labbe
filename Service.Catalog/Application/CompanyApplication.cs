using Service.Catalog.Dtos.Company;
using Service.Catalog.Repository.IRepository;
using Shared.Dictionary;
using Identidad.Api.Infraestructure.Services.IServices;
using Shared.Error;
using Service.Catalog.Mapper;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Service.Catalog.Dictionary;
using ClosedXML.Report;
using System;
using ClosedXML.Excel;
using Shared.Extensions;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary.Company;
using Shared.Helpers;
using Service.Catalog.Domain.Company;

namespace Service.Catalog.Application
{
    public class CompanyApplication : ICompanyApplication
    {
        private readonly ICompanyRepository _repository;

        public CompanyApplication(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CompanyListDto>> GetActive()
        {
            var catalogs = await _repository.GetActive();

            return catalogs.ToCompanyListDto();
        }


        public async Task<CompanyFormDto> GetById(Guid Id)
        {
            var Company = await _repository.GetById(Id);
            if (Company == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }
            return Company.ToCompanyFormDto();
        }
        public async Task<CompanyFormDto> Create(CompanyFormDto company)
        {
            Helpers.ValidateGuid(company.Id.ToString(), out Guid guid);

            var newIndication = company.ToModel();

            await CheckDuplicate(newIndication);

            await _repository.Create(newIndication);

            company = await GetById(newIndication.Id);

            return company;
        }

        public async Task<IEnumerable<CompanyListDto>> GetAll(string search = null)
        {
            var company = await _repository.GetAll(search);

            return company.ToCompanyListDto();
        }
        public async Task<CompanyFormDto> Update(CompanyFormDto company)
        {
            Helpers.ValidateGuid(company.Id.ToString(), out Guid guid);

            var existing = await _repository.GetById(company.Id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = company.ToModel(existing);
            await CheckDuplicate(updatedAgent);

            await _repository.Update(updatedAgent);

            return existing.ToCompanyFormDto();
        }

        public async Task<(byte[] file, string fileName)> ExportList(string search)
        {
            var company = await GetAll(search);

            var path = AssetsCompany.CompanyList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Compañias");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Compañias", company);

            template.Generate();

            var range = template.Workbook.Worksheet("Compañias").Range("Compañias");
            var table = template.Workbook.Worksheet("Compañias").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Catálogo de Compañias.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportForm(Guid id)
        {
            var company = await GetById(id);

            var path = AssetsCompany.CompanyForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Compañias");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Compañias", company);
            template.AddVariable("Contactos", company.Contacts);

            template.Generate();

            //var range = template.Workbook.Worksheet("Compañias").Range("Contactos");
            //var table = template.Workbook.Worksheet("Compañias").Range("$A$28:" + range.RangeAddress.LastAddress).CreateTable();
            //table.Theme = XLTableTheme.TableStyleMedium2; 

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Compañias (${company.Clave}).xlsx");
        }

        public string GeneratePassword()
        {

            return PasswordGenerator.GenerarPassword(8);
        }

        private async Task CheckDuplicate(Company company)
        {
            var isDuplicate = await _repository.IsDuplicate(company);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La Clave o el Nombre Comercial"));
            }
        }
    }
}
