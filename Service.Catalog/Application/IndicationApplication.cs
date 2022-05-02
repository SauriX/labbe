﻿using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Dtos.Indication;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class IndicationApplication : IIndicationApplication
    {

        private readonly IIndicationRepository _repository;

        public IndicationApplication(IIndicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IndicationFormDto> GetById(int Id)
        {
            var indication = await _repository.GetById(Id);
            if (indication == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }
            return indication.ToIndicationFormDto();
        }
        public async Task<IndicationFormDto> Create(IndicationFormDto indicacion)
        {
            var code = await ValidarClave(indicacion);

            if (indicacion.Id != 0 || code != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }

            var newIndication = indicacion.ToModel();

            var isDuplicate = await _repository.IsDuplicate(newIndication);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave "));
            }

            await _repository.Create(newIndication);

            indicacion = await GetById(newIndication.Id);

            return indicacion;
        }

        public async Task<IEnumerable<IndicationListDto>> GetAll(string search = null)
        {
            var indications = await _repository.GetAll(search);

            return indications.ToIndicationListDto();
        }
        public async Task<IndicationFormDto> Update(IndicationFormDto indication)
        {
            var existing = await _repository.GetById(indication.Id);

            var code = await ValidarClave(indication);
            if (existing.Clave != indication.Clave)
            {
                if (indication.Id != 0 || code != 0)
                {
                    throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
                }

            }

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = indication.ToModel(existing);

            var isDuplicate = await _repository.IsDuplicate(updatedAgent);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave "));
            }

            await _repository.Update(updatedAgent);

            return existing.ToIndicationFormDto();
        }

        public async Task<byte[]> ExportListIndication(string search = null)
        {
            var indication = await GetAll(search);

            var path = AssetsIndication.IndicationList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Indicaciones");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Indicaciones", indication);

            template.Generate();

            var range = template.Workbook.Worksheet("Indicaciones").Range("Indicaciones");
            var table = template.Workbook.Worksheet("Indicaciones").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return template.ToByteArray();
        }

        public async Task<byte[]> ExportFormIndication(int id)
        {
            var indication = await GetById(id);

            var path = AssetsIndication.IndicationForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Indicaciones");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Indicaciones", indication);
            template.AddVariable("Estudios", indication.Estudios);

            template.Generate();

            template.Format();

            return template.ToByteArray();
        }

        private async Task<int> ValidarClave(IndicationFormDto indication)
        {

            var clave = indication.Clave;

            var exists = await _repository.ValidateClave(clave);

            if (exists)
            {
                return 1;
            }

            return 0;
        }
    }

    
}
