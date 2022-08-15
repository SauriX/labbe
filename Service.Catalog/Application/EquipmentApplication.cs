using Service.Catalog.Application.IApplication;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Equipment;
using Service.Catalog.Dtos.Equipment;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
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
        public Task<(byte[] file, string fileName)> ExportForm(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<(byte[] file, string fileName)> ExportList(string search)
        {
            throw new System.NotImplementedException();
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
