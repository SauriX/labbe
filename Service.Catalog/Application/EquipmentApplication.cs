using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Equipment;
using Service.Catalog.Dtos.Equipment;
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
    public class EquipmentApplication : IEquipmentApplication
    {
        private readonly IEquipmentRepository _repository;
        public EquipmentApplication(IEquipmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<EquipmentListDto> Create(EquipmentFormDto equipment)
        {
            if (string.IsNullOrEmpty(equipment.Id.ToString()))
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newEquipment = equipment.ToModel();

            await CheckDuplicate(newEquipment);

            await _repository.Create(newEquipment);

            return newEquipment.ToEquipmentListDto();
        }


        public async Task<IEnumerable<EquipmentListDto>> GetAll(string search)
        {
            var equipment = await _repository.GetAll(search);

            return equipment.ToEquipmentListDto();
        }

        public async Task<EquipmentFormDto> GetById(int Id)
        {
            var equipment = await _repository.GetById(Id);

            if (equipment == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return equipment.ToEquipmentFormDto();
        }

        public async Task<EquipmentListDto> Update(EquipmentFormDto equipment)
        {
            var existing = await _repository.GetById(equipment.Id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedEquipmentn = equipment.ToModel(existing);

            await CheckDuplicate(updatedEquipmentn);

            await _repository.Update(updatedEquipmentn);

            return updatedEquipmentn.ToEquipmentListDto();
        }
        public async Task<(byte[] file, string fileName)> ExportForm(int id)
        {
            var equipment = await GetById(id);

            var path = Assets.EquipmentForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Equipos");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Sucursales", equipment);

            template.Generate();

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Sucursales ({equipment.Clave}).xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportList(string search)
        {
            
            var equipment = await GetAll(search);

            var path = Assets.EquipmentList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Equipos");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Sucursales", equipment);

            template.Generate();

            var range = template.Workbook.Worksheet("Sucursales").Range("Sucursales");
            var table = template.Workbook.Worksheet("Sucursales").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Catálogo de Equipos.xlsx");
        }
        private async Task CheckDuplicate(Equipos equipment)
        {
            var isDuplicate = await _repository.IsDuplicate(equipment);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }
        }
    }
}
