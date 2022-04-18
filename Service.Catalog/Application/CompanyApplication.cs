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

namespace Service.Catalog.Application
{
    public class CompanyApplication : ICompanyApplication
    {
        private readonly ICompanyRepository _repository;

        public CompanyApplication(ICompanyRepository repository)
        {
            _repository = repository;
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
            if (company.IdCompania != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
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
            var existing = await _repository.GetById(company.IdCompania);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = company.ToModel(existing);

            await _repository.Update(updatedAgent);

            return existing.ToCompanyFormDto();
        }

        public async Task<byte[]> ExportListIndication(string search = null)
        {
            var company = await GetAll(search);

            var path = AssetsIndication.IndicationList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Indicaciones");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Indicaciones", company);

            template.Generate();

            var range = template.Workbook.Worksheet("Indicaciones").Range("Indicaciones");
            var table = template.Workbook.Worksheet("Indicaciones").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return template.ToByteArray();
        }

        public async Task<byte[]> ExportFormIndication(int id)
        {
            var company = await GetById(id);

            var path = AssetsIndication.IndicationForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Indicaciones");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Indicaciones", company);

            template.Generate();

            template.Format();

            return template.ToByteArray();
        }
    }
}