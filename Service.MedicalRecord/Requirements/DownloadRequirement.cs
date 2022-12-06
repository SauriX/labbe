using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Service.MedicalRecord.Client.IClient;
using Shared.Dictionary;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Requirements
{
    public class DownloadRequirement : IAuthorizationRequirement
    {
    }

    public class DownloadRequirementHandler : AuthorizationHandler<DownloadRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityClient _identityClient;

        public DownloadRequirementHandler(IHttpContextAccessor httpContextAccessor, IIdentityClient identityClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityClient = identityClient;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DownloadRequirement requirement)
        {
            var claims = _httpContextAccessor.HttpContext.User?.Claims;

            var id = claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var name = claims?.SingleOrDefault(x => x.Type == CustomClaims.FullName)?.Value;

            if (id == null || name == null) return;

            var userId = Guid.Parse(id);
            var userName = name.ToString();

            //var controller = _httpContextAccessor.HttpContext.Request.RouteValues["controller"].ToString();

            //var scopes = await _identityClient.GetScopes(controller);

            //var hasPermission = scopes.Crear;

            if (true)
            {
                _httpContextAccessor.HttpContext.Items["userId"] = userId;
                _httpContextAccessor.HttpContext.Items["userName"] = userName;
                context.Succeed(requirement);
            }

            return;
        }
    }
}
