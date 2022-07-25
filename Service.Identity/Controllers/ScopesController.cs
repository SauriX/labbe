using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Application.IApplication;
using Service.Identity.Dictionary;
using Service.Identity.Dtos;
using Service.Identity.Dtos.Scopes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScopesController : ControllerBase
    {
        private readonly IProfileApplication _service;

        public ScopesController(IProfileApplication service)
        {
            _service = service;
        }

        [HttpGet(ControllerNames.Role)]
        public async Task<ScopesDto> GetRoleScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Role);
        }

        [HttpGet(ControllerNames.User)]
        public async Task<ScopesDto> GetUserScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.User);
        }

        [HttpGet(ControllerNames.Branch)]
        public async Task<ScopesDto> GetBranchScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Branch);
        }

        [HttpGet(ControllerNames.Company)]
        public async Task<ScopesDto> GetCompanyScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Company);
        }

        [HttpGet(ControllerNames.Medic)]
        public async Task<ScopesDto> GetMedicScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Medic);
        }

        [HttpGet(ControllerNames.Study)]
        public async Task<ScopesDto> GetStudyScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Study);
        }

        [HttpGet(ControllerNames.Reagent)]
        public async Task<ScopesDto> GetReagentScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Reagent);
        }

        [HttpGet(ControllerNames.Indication)]
        public async Task<ScopesDto> GetIndicationScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Indication);
        }

        [HttpGet(ControllerNames.Parameter)]
        public async Task<ScopesDto> GetParameterScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Parameter);
        }

        [HttpGet(ControllerNames.Catalog)]
        public async Task<ScopesDto> GetCatalogScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Catalog);
        }

        [HttpGet(ControllerNames.Price)]
        public async Task<ScopesDto> GetPriceScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Price);
        }

        [HttpGet(ControllerNames.Pack)]
        public async Task<ScopesDto> GetPackScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Pack);
        }

        [HttpGet(ControllerNames.Promo)]
        public async Task<ScopesDto> GetPromoScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Promo);
        }

        [HttpGet(ControllerNames.Loyalty)]
        public async Task<ScopesDto> GetLoyaltyScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Loyalty);
        }

        [HttpGet(ControllerNames.Tag)]
        public async Task<ScopesDto> GetTagScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Tag);
        }

        [HttpGet(ControllerNames.Route)]
        public async Task<ScopesDto> GetRouteScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Route);
        }

        [HttpGet(ControllerNames.Maquila)]
        public async Task<ScopesDto> GetMaquilaScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Maquila);
        }
        [HttpGet(ControllerNames.MedicalRecord)]
        public async Task<ScopesDto> GetMedicalRecordScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Maquila);
        }
        [HttpGet(ControllerNames.Appointment)]
        public async Task<ScopesDto> GetAppointmentScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Appointment);
        }
        [HttpGet(ControllerNames.Configuration)]
        public async Task<ScopesDto> GetConfigurationScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Configuration);
        }

        [HttpGet(ControllerNames.Request)]
        public async Task<ScopesDto> GetRequestScopes()
        {
            var userId = GetUserId();
            return await _service.GetScopes(userId, ControllerNames.Request);
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        }
    }

}
