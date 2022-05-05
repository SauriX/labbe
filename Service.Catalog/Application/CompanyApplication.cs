﻿using Service.Catalog.Dtos.Company;
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


        public async Task<CompanyFormDto> GetById(int Id)
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
            var code = await ValidarClaveNombre(company);

            if (company.Id != 0 || code != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }

            var newIndication = company.ToModel();

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
            var existing = await _repository.GetById(company.Id);

            var code = await ValidarClaveNombre(company);
            if (existing.Clave != company.Clave || existing.NombreComercial != company.NombreComercial)
            {
                if (company.Id != 0 || code != 0)
                {
                    throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
                }

            }

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = company.ToModel(existing);

            await _repository.Update(updatedAgent);

            return existing.ToCompanyFormDto();
        }

        public async Task<byte[]> ExportListCompany(string search = null)
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

            return template.ToByteArray();
        }

        public async Task<byte[]> ExportFormCompany(int id)
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

            //template.Format();

            return template.ToByteArray();
        }

        public string GeneratePassword()
        {

            return PasswordGenerator.GenerarPassword(8);
        }

        private async Task<int> ValidarClaveNombre(CompanyFormDto company)
        {

            var clave = company.Clave;
            var name = company.NombreComercial;

            var exists = await _repository.ValidateClaveNamne(clave, name);

            if (exists)
            {
                return 1;
            }

            return 0;
        }
    }
}
