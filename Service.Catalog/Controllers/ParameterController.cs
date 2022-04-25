using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Parameters;
using System.Threading.Tasks;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParameterController : ControllerBase
    {
        private readonly IParameterApplication _ParameterService;

        public ParameterController(IParameterApplication indicationService)
        {
            _ParameterService = indicationService;
        }

        [HttpPost]
        public async Task Create(ParameterForm Parameter)
        {
            await _ParameterService.Create(Parameter);
        }


    }
}
