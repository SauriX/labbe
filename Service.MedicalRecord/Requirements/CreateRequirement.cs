using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Service.MedicalRecord.Client.IClient;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Requirements
{
    public class CreateRequirement : IAuthorizationRequirement
    {
    }

    public class CreateRequirementHandler : AuthorizationHandler<CreateRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityClient _identityClient;

        public CreateRequirementHandler(IHttpContextAccessor httpContextAccessor, IIdentityClient identityClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityClient = identityClient;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateRequirement requirement)
        {
            var id = _httpContextAccessor.HttpContext.User?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (id == null) return;

            var userId = Guid.Parse(id);
            //var controller = _httpContextAccessor.HttpContext.Request.RouteValues["controller"].ToString();

            //var scopes = await _identityClient.GetScopes(controller);

            //var hasPermission = scopes.Crear;

            if (true)
            {
                _httpContextAccessor.HttpContext.Items["userId"] = userId;
                context.Succeed(requirement);
            }

            return;
        }
    }
}
