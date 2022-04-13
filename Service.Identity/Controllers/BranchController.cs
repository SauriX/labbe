using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Repository.IRepository;

namespace Service.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchRepository _service;
        public BranchController(IBranchRepository service)
        {
            _service = service;
        }
    
    }
}
