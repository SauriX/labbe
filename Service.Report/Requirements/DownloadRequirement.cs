﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Service.Report.Client.IClient;
using Shared.Dictionary;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.Report.Requirements
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
            var id = _httpContextAccessor.HttpContext.User?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (id == null) return;

            var fullname = _httpContextAccessor.HttpContext.User?.Claims?.SingleOrDefault(x => x.Type == CustomClaims.FullName)?.Value;
            if (fullname == null) return;

            var userId = Guid.Parse(id);
            var controller = _httpContextAccessor.HttpContext.Request.RouteValues["controller"].ToString();

            var scopes = await _identityClient.GetScopes(controller);

            var hasPermission = scopes.Descargar;

            if (hasPermission)
            {
                _httpContextAccessor.HttpContext.Items["userId"] = userId;
                _httpContextAccessor.HttpContext.Items["fullname"] = fullname;
                context.Succeed(requirement);
            }

            return;
        }
    }
}
