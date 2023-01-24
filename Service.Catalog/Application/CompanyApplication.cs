using ClosedXML.Excel;
using ClosedXML.Report;
using EventBus.Messages.Catalog;
using MassTransit;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary.Company;
using Service.Catalog.Domain.Company;
using Service.Catalog.Dtos.Company;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class CompanyApplication : ICompanyApplication
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ICompanyRepository _repository;

        public CompanyApplication(IPublishEndpoint publishEndpoint, ICompanyRepository repository)
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
        }

        public async Task<IEnumerable<CompanyListDto>> GetActive()
        {
            var catalogs = await _repository.GetActive();

            return catalogs.ToCompanyListDto();
        }

        public async Task<CompanyFormDto> GetById(Guid Id)
        {
            var company = await _repository.GetById(Id);

            if (company == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return company.ToCompanyFormDto();
        }

        public async Task<CompanyFormDto> Create(CompanyFormDto company)
        {
            Helpers.ValidateGuid(company.Id.ToString(), out Guid guid);

            var newIndication = company.ToModel();

            await CheckDuplicate(newIndication);

            CheckDuplicateContact(newIndication.Contacts);

            await _repository.Create(newIndication);

            var contract = new CompanyContract(newIndication.Id, newIndication.Clave, newIndication.NombreComercial);

            await _publishEndpoint.Publish(contract);

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

            CheckDuplicateContact(updatedAgent.Contacts);

            await _repository.Update(updatedAgent);

            var contract = new CompanyContract(updatedAgent.Id, updatedAgent.Clave, updatedAgent.NombreComercial);

            await _publishEndpoint.Publish(contract);

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

        private void CheckDuplicateContact(ICollection<Contact> contact)
        {
            var duplicates = contact.GroupBy(x => x.Nombre).Any(g => g.Count() > 1);

            if (duplicates)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("El Nombre del contacto"));
            }
        }

        public async Task<List<ContactListDto>> GetContactsByCompany(Guid Id)
        {
            var company = await _repository.GetById(Id);

            if (company == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return company.Contacts.ToContactListDto().ToList();
        }
    }
}
