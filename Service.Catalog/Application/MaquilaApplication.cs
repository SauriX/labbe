using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Domain.Maquila;
using Service.Catalog.Dtos.Maquilador;
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
    public class MaquilaApplication : IMaquilaApplication
    {
        private readonly IMaquilaRepository _repository;

        public MaquilaApplication(IMaquilaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MaquilaListDto>> GetAll(string search)
        {
            var maquilas = await _repository.GetAll(search);

            return maquilas.ToMaquilaListDto();
        }

        public async Task<IEnumerable<MaquilaListDto>> GetActive()
        {
            var maquilas = await _repository.GetActive();

            return maquilas.ToMaquilaListDto();
        }

        public async Task<MaquilaFormDto> GetById(int id)
        {
            var maquila = await _repository.GetById(id);

            if (maquila == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return maquila.ToMaquilaFormDto();
        }

        public async Task<MaquilaListDto> Create(MaquilaFormDto maquila)
        {
            if (maquila.Id != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newMaquila = maquila.ToModel();

            await CheckDuplicate(newMaquila);

            await _repository.Create(newMaquila);

            newMaquila = await _repository.GetById(newMaquila.Id);

            return newMaquila.ToMaquilaListDto();
        }

        public async Task<MaquilaListDto> Update(MaquilaFormDto maquila)
        {
            var existing = await _repository.GetById(maquila.Id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedMaquila = maquila.ToModel(existing);

            await CheckDuplicate(updatedMaquila);

            await _repository.Update(updatedMaquila);

            updatedMaquila = await _repository.GetById(updatedMaquila.Id);

            return updatedMaquila.ToMaquilaListDto();
        }

        public async Task<(byte[] file, string fileName)> ExportList(string search)
        {
            var maquilas = await GetAll(search);

            var path = Assets.MaquiladorList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Maquilador");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Maquilador", maquilas);

            template.Generate();

            var range = template.Workbook.Worksheet("Maquilador").Range("Maquilador");
            var table = template.Workbook.Worksheet("Maquilador").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Catálogo de Maquilador.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportForm(int id)
        {
            var maquila = await GetById(id);

            var path = Assets.MaquiladorForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Maquilador");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Maquilador", maquila);


            template.Generate();

            return (template.ToByteArray(), $"Catálogo de Maquilador ({maquila.Clave}).xlsx");
        }

        private async Task CheckDuplicate(Maquila maquila)
        {
            var isDuplicate = await _repository.IsDuplicate(maquila);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave, nombre o correo"));
            }
        }
    }
}
