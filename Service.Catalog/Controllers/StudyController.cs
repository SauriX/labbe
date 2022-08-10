using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Study;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyController : ControllerBase
    {
        private readonly IStudyApplication _Service;

        public StudyController(IStudyApplication Service)
        {
            _Service = Service;
        }

        [HttpGet("{id}")]
        [Authorize(Policies.Access)]
        public async Task<StudyFormDto> GetById(int id)
        {
            return await _Service.GetById(id);
        }      
        
        [HttpPost("multiple")]
        public async Task<IEnumerable<StudyListDto>> GetByIds(List<int> ids)
        {
            return await _Service.GetByIds(ids);
        }

        [HttpGet("all/{search?}")]
        [Authorize(Policies.Access)]
        public async Task<IEnumerable<StudyListDto>> GetAll(string search = null)
        {
            return await _Service.GetAll(search);
        }

        [HttpGet("active")]
        public async Task<IEnumerable<StudyListDto>> GetActive()
        {
            return await _Service.GetActive();
        }

        [HttpPost]
        [Authorize(Policies.Create)]
        public async Task<StudyFormDto> Create(StudyFormDto study)
        {
            study.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _Service.Create(study);
        }
        [HttpPut]
        [Authorize(Policies.Update)]
        public async Task<StudyFormDto> Update(StudyFormDto study)
        {
            study.UsuarioId = (Guid)HttpContext.Items["userId"];
            return await _Service.Update(study);
        }


        [HttpPost("export/list/{search?}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportList(string search = null)
        {
            var (file, fileName) = await _Service.ExportList(search);
            return File(file, MimeType.XLSX, fileName);
        }

        [HttpPost("export/form/{id}")]
        [Authorize(Policies.Download)]
        public async Task<IActionResult> ExportForm(int id)
        {
            var (file, fileName) = await _Service.ExportForm(id);
            return File(file, MimeType.XLSX, fileName);
        }
    }

}

