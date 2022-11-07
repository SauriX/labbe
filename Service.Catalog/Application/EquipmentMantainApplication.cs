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
            var clave = $"M{equipment.Nombre}-{mantain.Fecha.Day}{mantain.Fecha.Month}{mantain.Fecha.Year}";
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
        public async Task<string> SaveImage(MantainImageDto requestDto)
        {
    
                var request = await GetById(requestDto.ExpedienteId);

                var typeOk = requestDto.Tipo.In("orden", "ine", "ineReverso", "formato");

                var isImage = requestDto.Imagen.IsImage();

                if (!typeOk || !isImage)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, SharedResponses.InvalidImage);
                }

                requestDto.Clave = request.Clave;


                    //var name = Helpers.GenerateRandomHex();
                    var fileName = requestDto.Imagen.FileName;
                    var name = fileName[..fileName.LastIndexOf(".")];

                    var existingImage = await _service.GetImage(requestDto.SolicitudId, name);

                    var path = await SaveImageGetPath(requestDto, existingImage?.Clave?? name);

                    var image = new MantainImages(existingImage?.Id ?? 0, requestDto.SolicitudId,existingImage?.Clave ?? name, path, "format")
                    {
                        UsuarioCreoId = existingImage?.UsuarioCreoId ?? requestDto.UsuarioId,
                        FechaCreo = existingImage?.FechaCreo ?? DateTime.Now,
                        UsuarioModificoId = existingImage == null ? null : requestDto.UsuarioId,
                        FechaModifico = existingImage == null ? null : DateTime.Now
                    };

                    await _service.UpdateImage(image);

                    return image.Clave;
                
               
            
        }
        private static async Task<string> SaveImageGetPath(MantainImageDto requestDto, string fileName = null)
        {
            var path = Path.Combine("wwwroot/images/mantain", requestDto.Clave);
            var name = string.Concat(fileName ?? requestDto.Tipo, ".png");

            var isSaved = await requestDto.Imagen.SaveFileAsync(path, name);

            if (isSaved)
            {
                return Path.Combine(path, name);
            }

            return null;
        }

    }
}
