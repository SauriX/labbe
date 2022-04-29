using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
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
    public class MaquiladorApplication : IMaquiladorApplication
    {
        private readonly IMaquiladorRepository _repository;

        public MaquiladorApplication(IMaquiladorRepository repository)
        {
            _repository = repository;
        }

        //public async Task<IEnumerable<MaquiladorApplicationListDto>> GetActive()
        //{
        //    var catalogs = await _repository.GetActive();

        //    return catalogs.ToMaquiladorListDto();
        //}


        public async Task<MaquiladorFormDto> GetById(int Id)
        {
            var maqui = await _repository.GetById(Id);
            if (maqui == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }
            return maqui.ToMaquiladorFormDto();
        }
        public async Task<MaquiladorFormDto> Create(MaquiladorFormDto maqui)
        {
            var code = await ValidarClaveNombre(maqui);

            if (maqui.Id != 0 || code != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }

            var newIndication = maqui.ToModel();

            await _repository.Create(newIndication);

            maqui = await GetById(newIndication.Id);

            return maqui;
        }

        public async Task<IEnumerable<MaquiladorListDto>> GetAll(string search = null)
        {
            var maqui = await _repository.GetAll(search);

            return maqui.ToMaquiladorListDto();
        }
        public async Task<MaquiladorFormDto> Update(MaquiladorFormDto maqui)
        {
            var existing = await _repository.GetById(maqui.Id);

            var code = await ValidarClaveNombre(maqui);
            if (existing.Clave != maqui.Clave || existing.Nombre != maqui.Nombre)
            {
                if (maqui.Id != 0 || code != 0)
                {
                    throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
                }

            }

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = maqui.ToModel(existing);

            await _repository.Update(updatedAgent);

            return existing.ToMaquiladorFormDto();
        }

        public async Task<byte[]> ExportListMaquilador(string search = null)
        {
            var maqui = await GetAll(search);

            var path = Assets.MaquiladorList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Maquilador");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Maquilador", maqui);

            template.Generate();

            var range = template.Workbook.Worksheet("Maquilador").Range("Maquilador");
            var table = template.Workbook.Worksheet("Maquilador").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return template.ToByteArray();
        }

        public async Task<byte[]> ExportFormMaquilador(int id)
        {
            var company = await GetById(id);

            var path = Assets.MaquiladorForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Maquilador");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Maquilador", company);

            template.Generate();

            return template.ToByteArray();
        }


        private async Task<int> ValidarClaveNombre(MaquiladorFormDto maqui)
        {

            var clave = maqui.Clave;
            var name = maqui.Nombre;

            var exists = await _repository.ValidateClaveName(clave, name);

            if (exists)
            {
                return 1;
            }

            return 0;
        }
    }
}
