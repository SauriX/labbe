using ClosedXML.Excel;
using ClosedXML.Report;
using MassTransit;
using MassTransit.Util;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Loyalty;
using Service.Catalog.Dtos.Catalog;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class BudgetApplication : IBudgetApplication
    {
        private readonly IBudgetRepository _repository;

        public BudgetApplication(IBudgetRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BudgetListDto>> GetAll(string search = null)
        {
            var budgets = await _repository.GetAll(search);

            return budgets.ToBudgetListDto();
        }

        public async Task<BudgetFormDto> GetById(int id)
        {
            var budget = await _repository.GetById(id);

            if (budget == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return budget.ToBudgetFormDto();
        }

        public async Task<IEnumerable<BudgetListDto>> GetActive()
        {
            var budgets = await _repository.GetActive();

            return budgets.ToBudgetSelectInputDto();
        }

        public async Task<IEnumerable<BudgetListDto>> GetBudgetByBranch(Guid branchId)
        {
            var budgets = await _repository.GetBudgetByBranch(branchId);

            return budgets.ToBranchBudgetListDto();
        }

        public async Task<IEnumerable<BudgetListDto>> GetBudgetsByBranch(BudgetFilterDto search)
        {
            var budgets = await _repository.GetBudgetsByBranch(search);

            return budgets.ToBranchBudgetListDto();
        }

        public async Task<BudgetListDto> Create(BudgetFormDto budget)
        {            
            if (budget.Id != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newBudget = budget.ToModel();

            await CheckDuplicate(newBudget);

            await _repository.Create(newBudget);

            newBudget = await _repository.GetById(newBudget.Id);

            return newBudget.ToBudgetListDto();
        }

        public async Task CreateList(List<BudgetFormDto> budgets)
        {
            var newBudget = budgets.ToModelList();

            await _repository.CreateList(newBudget);
        }

        public async Task<BudgetListDto> Update(BudgetFormDto budget)
        {
            var existing = await _repository.GetById(budget.Id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updateBudget = budget.ToModel(existing);

            await CheckDuplicate(updateBudget);

            await _repository.Update(updateBudget);

            updateBudget = await _repository.GetById(updateBudget.Id);

            return updateBudget.ToBudgetListDto();
        }

        public async Task UpdateService(ServiceUpdateDto service, Guid userId)
        {
            var existing = await _repository.GetById(service.Id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            existing.CostoFijo = service.CostoFijo;
            existing.UsuarioModificoId = userId;

            await _repository.Update(existing);
        }

        public async Task<byte[]> ExportList(string search)
        {
            var catalogs = await GetAll(search);

            var path = Assets.BudgetList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Áreas");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Catalogo", catalogs);

            template.Generate();

            var range = template.Workbook.Worksheet("Catálogos").Range("Catalogos");
            var table = template.Workbook.Worksheet("Catálogos").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return template.ToByteArray();
        }

        public async Task<(byte[] file, string code)> ExportForm(int id)
        {
            var catalog = await GetById(id);

            var path = Assets.BudgetForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Áreas");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Catalogo", catalog);

            template.Generate();

            template.Format();

            return (template.ToByteArray(), catalog.Clave);
        }

        private async Task CheckDuplicate(Budget budget)
        {
            var isDuplicate = await _repository.IsDuplicate(budget);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave"));
            }
        }
    }
}
