using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Dtos.WorkList;
using Shared.Dictionary;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkListController : ControllerBase
    {
        private readonly IWorkListApplication _service;

        public WorkListController(IWorkListApplication service)
        {
            _service = service;
        }

        [HttpPost("print")]
        //[Authorize(Policies.Print)]
        public async Task<IActionResult> PrintOrder(WorkListFilterDto filter)
        {
            var file = await _service.PrintWorkList(filter);

            return File(file, MimeType.PDF, "worklist.pdf");
        }
    }
}
