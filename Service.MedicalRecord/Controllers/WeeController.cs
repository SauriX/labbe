using Integration.WeeClinic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class WeeController : ControllerBase
    {
        [HttpGet("login")]
        public async Task<string> Login()
        {
            return await Base.Login();
        }
    }
}
