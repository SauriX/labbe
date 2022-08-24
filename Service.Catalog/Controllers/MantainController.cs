using ClosedXML.Report.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Equipmentmantain;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantainController : ControllerBase
    {
        private readonly IEquipmentMantainApplication _service;

        public MantainController(IEquipmentMantainApplication service)
        {
            _service = service;
        }

        [HttpPost("all")]
        [Authorize(Policies.Access)]
        public async Task<List<MantainListDto>> GetAll(MantainSearchDto search)
        {
            return await _service.GetAll(search);
        }

        /*[HttpGet("active")]
        public async Task<List<MaquilaListDto>> GetActive()
        {
            return await _service.GetActive();
        }*/

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<MantainFormDto> GetById(Guid id)
        {
            return await _service.GetById(id);
        }
        [HttpGet("equipo/{id}")]
        [Authorize(Policies.Access)]
        public async Task<EquimentDetailDto> Getequipo(int id)
        {
            return await _service.Getequip(id);
        }
        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<MantainListDto> Create(MantainFormDto maquila)
        {
            maquila.IdUser = (Guid)HttpContext.Items["userId"];
            return await _service.  Create(maquila);
        }

        [HttpPut]
        [Authorize(Policies.Update)]    
        public async Task<MantainListDto> Update(MantainFormDto maquila)
        {
            maquila.IdUser = (Guid)HttpContext.Items["userId"];
            return await _service.Update(maquila);
        }

        [HttpPut("images")]
        [Authorize(Policies.Update)]
        public async Task<bool> SaveImage([FromForm] MantainImageDto[] requestDto)
        {

            List<MantainImageDto> request = new List<MantainImageDto>();
            foreach (var image in requestDto) {
                image.UsuarioId = (Guid)HttpContext.Items["userId"];
                request.Add(image);
            }
           return await _service.SaveImage(request.ToArray());
        }
        [HttpPost("order/{Id}")]
        //[Authorize(Policies.Print)]
        public async Task<IActionResult> PrintOrder(Guid Id)
        {
            var file = await _service.Print(Id);

            return File(file, MimeType.PDF, "order.pdf");
        }

    }
}
