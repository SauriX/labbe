using Integration.Contpaqi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Integration.Contpaqi.Service.IService;

namespace Service.Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ISessionService _service;

        public TestController(ISessionService service)
        {
            _service = service;
        }

        [HttpGet]
        public bool Init()
        {
            _service.InitConnection();
            return true;
        }
    }
}