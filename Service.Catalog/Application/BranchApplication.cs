using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Domain.Branch;
using Service.Catalog.Dtos.Branch;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class BranchApplication : IBranchApplication
    {
        public readonly IBranchRepository _repository;
        public BranchApplication(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Create(BranchFormDto branch)
        {
            if (!string.IsNullOrEmpty(branch.idSucursal))
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newBranch = branch.ToModel();

            var (isDuplicate, code) = await _repository.IsDuplicate(newBranch);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated(Responses.Duplicated($"El {code}")));
            }

            await _repository.Create(newBranch);

            return true;
        }
        public async Task<BranchFormDto> GetById(string Id)
        {
            var branch = await _repository.GetById(Id);
            if (branch == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }
            //var studyes = await _repository.getservicios(Id);
            return branch.ToBranchFormDto();
        }

        public async Task<bool> Update(BranchFormDto branch)
        {
            var existing = await _repository.GetById(branch.idSucursal);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = branch.ToModel(existing);

            var (isDuplicate, code) = await _repository.IsDuplicate(updatedAgent);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated($"El {code}"));
            }

            var isMAtrisActive = await _repository.isMatrizActive(updatedAgent);

            if (isMAtrisActive)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated($"Ya exsite ubna matriz activa"));
            }
            await _repository.Update(updatedAgent);

            return true;
        }

        public async Task<IEnumerable<BranchInfoDto>> GetAll(string search = null)
        {
            var branch = await _repository.GetAll(search);
            return branch.ToBranchListDto();
        }

        public async Task<(byte[] file, string fileName)> ExportListBranch(string search = null)
        {
            var indication = await GetAll(search);

            var path = Assets.BranchList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Sucursales");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Sucursales", indication);

            template.Generate();

            var range = template.Workbook.Worksheet("Sucursales").Range("Sucursales");
            var table = template.Workbook.Worksheet("Sucursales").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Catálogo de Sucursales.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportFormBranch(string id)
        {
            var indication = await GetById(id);

            var path = Assets.BranchtForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Sucursales");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Sucursales", indication);

            template.Generate();

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Sucursales ({indication.clave}).xlsx");
        }

        public async Task<IEnumerable<BranchCityDto>> GetBranchByCity()
        {
            var branch = await _repository.GetBranchByCity();
            var results = from c in branch
                          group c by c.Ciudad into grupo
                          select new BranchCityDto
                          {
                              Ciudad = grupo.Key,
                              Sucursales = grupo.ToList().ToBranchListDto(),
                          };


            return results; 
        }
    }

}
