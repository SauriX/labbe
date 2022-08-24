using ClosedXML.Report.Utils;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Domain.EquipmentMantain;
using Service.Catalog.Dtos.Equipmentmantain;
using Service.Catalog.Mapper;
using Service.Catalog.Repository;
using Service.Catalog.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SharedResponses = Shared.Dictionary.Responses;

namespace Service.Catalog.Application
{
    public class EquipmentMantainApplication: IEquipmentMantainApplication
    {
        private readonly IEquipmentMantainRepository _service;
        private readonly IPdfClient _pdfClient;
        public EquipmentMantainApplication( IPdfClient pdfClient, IEquipmentMantainRepository service)
        {
            _service = service;
            _pdfClient = pdfClient;
        }

        public async Task<List<MantainListDto>> GetAll(MantainSearchDto search)
        {
              var mantains = await _service.GetAll(search);

            return mantains.ToMantainListDto();
        }
        public async Task<MantainFormDto> GetById(Guid Id)
        {
            var mantain = await _service.GetById(Id);

            return mantain.ToMaquilaFormDto();
        }
        public async Task<EquimentDetailDto> Getequip(int Id)
            {
            var mantain = await _service.GetEquip(Id);

            return mantain.ToDetailDto();
        }
        public async Task<MantainListDto> Create(MantainFormDto mantain)
        {
            var newParameter = mantain.ToModel();
            var equipment = await _service.GetEquip(mantain.ide);
            var clave = $"M{equipment.Nombre}{mantain.Fecha.Day}{mantain.Fecha.Month}{mantain.Fecha.Year}";
            newParameter.clave = clave;

            newParameter.Num_Serie = equipment.Valores.AsQueryable().Last().Num_Serie.ToString();
            // await CheckDuplicate(newParameter);
            // await CheckPromotionPackActive(newParameter);
            await _service.Create(newParameter);

            newParameter = await _service.GetById(newParameter.Id);
            
            return newParameter.ToMantainListDto();
        }

        public async Task<MantainListDto> Update(MantainFormDto mantain)
        {
                var Id = Guid.Parse(mantain.Id);
            var existing = await _service.GetById(Id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedParameter = mantain.ToModel(existing);

            //await CheckDuplicate(updatedParameter);
            //await CheckPromotionPackActive(updatedParameter);
            await _service.Update(updatedParameter);

            updatedParameter = await _service.GetById(updatedParameter.Id);

            return updatedParameter.ToMantainListDto();
        }
        public async Task<byte[]> Print(Guid Id)
        {
            var request = await _service.GetById(Id);

            if (request == null || request.Id != Id)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }


           var mantain= request.ToMaquilaFormDto();
            return await _pdfClient.GenerateOrder(mantain);
        }
        public async Task<bool> SaveImage(MantainImageDto[] requestDto)
        {
            List<MantainImages> images = new List<MantainImages>();
            
            foreach (var item in requestDto) {
                var request = await GetById(item.SolicitudId);
                var Id = Guid.Parse(request.Id);
                var existing = await _service.GetById(Id);
                var updateiTEM = request.ToModel(existing);
                var typeOk = item.Tipo.In("orden", "ine");

                var isImage = item.Imagen.IsImage();

                if (!typeOk || !isImage)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, SharedResponses.InvalidImage);
                }
                var path = await SaveImageGetPath(item);
                var image = new MantainImages
                {
                    Id = Guid.NewGuid(),
                    UrlImg = path,
                    MantainId = Guid.Parse(request.Id),
                };
                images.Add(image);
            }
            await _service.AddImage(images, requestDto[0].SolicitudId);
            return true;
        }

        private static async Task<string> SaveImageGetPath(MantainImageDto requestDto)
        {
            var path = Path.Combine("http://localhost:20347/images/mantain", $"{requestDto.clave}{requestDto.SolicitudId}");
            var name = string.Concat(requestDto.Tipo, ".png");
            var path2 = Path.Combine("wwwroot/images/mantain", requestDto.clave);
            var isSaved = await requestDto.Imagen.SaveFileAsync(path2, name);

            if (isSaved)
            {
                return Path.Combine(path, name);
            }

            return null;
        }
    }
}
