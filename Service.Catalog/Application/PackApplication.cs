using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Domain.Packet;
using Service.Catalog.Dtos.Pack;
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
    public class PackApplication : IPackApplication
    {
        private readonly IPackRepository _repository;
        public PackApplication(IPackRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<PackListDto>> GetAll(string search)
        {
            var packs = await _repository.GetAll(search);

            return packs.ToPackListDto();
        }
        
        public async Task<IEnumerable<PackListDto>> GetPackList(string search)
        {
            var packs = await _repository.GetPackList(search);

            return packs.ToPackListPDto();
        }

        public async Task<IEnumerable<PackListDto>> GetActive()
        {
            var packs = await _repository.GetActive();

            return packs.ToPackListDto();
        }

        public async Task<PackFormDto> GetById(int id)
        {
            //Helpers.ValidateGuid(id, out Guid guid);

            var pack = await _repository.GetById(id);

            if (pack == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return pack.ToPackFormDto();
        }

        public async Task<PackListDto> Create(PackFormDto pack)
        {
            /*if (!string.IsNullOrEmpty(pack.Id))
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }*/

            var newPack = pack.ToModel();

            await CheckDuplicate(newPack);

            await _repository.Create(newPack);

            newPack = await _repository.GetById(newPack.Id);

            return newPack.ToPackListDto();
        }

        public async Task<PackListDto> Update(PackFormDto pack)
        {
            // Helpers.ValidateGuid(parameter.Id, out Guid guid);

            var existing = await _repository.GetById(pack.Id);

            /*if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }*/

            var updatedPack = pack.ToModel(existing);

            await CheckDuplicate(updatedPack);

            await _repository.Update(updatedPack);

            updatedPack = await _repository.GetById(updatedPack.Id);

            return updatedPack.ToPackListDto();
        }
        public async Task<(byte[] file, string fileName)> ExportList(string search)
        {
            var parameters = await GetAll(search);

            var path = Assets.PackList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Paquetes");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Paquetes", parameters);

            template.Generate();

            var range = template.Workbook.Worksheet("Paquetes").Range("Paquetes");
            var table = template.Workbook.Worksheet("Paquetes").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Catálogo de Paquetes.xlsx");
        }
        public async Task<(byte[] file, string fileName)> ExportForm(int id)
        {
            var parameter = await GetById(id);
            var path = Assets.PackForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Paquete");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Paquete", parameter);
            template.AddVariable("Estudios", parameter.Estudio);
            template.Generate();

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Paquetes ({parameter.Clave}).xlsx");
        }
        private async Task CheckDuplicate(Packet pack)
        {
            var isDuplicate = await _repository.IsDuplicate(pack);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }
        }
    }
}
