﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Service.Identity.Application.IApplication;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.Identity.Requirements
{
    public class CreateRequirement : IAuthorizationRequirement
    {
    }

    public class CreateRequirementHandler : AuthorizationHandler<CreateRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProfileApplication _scopesApplication;

        public CreateRequirementHandler(IHttpContextAccessor httpContextAccessor, IProfileApplication scopesApplication)
        {
            _httpContextAccessor = httpContextAccessor;
            _scopesApplication = scopesApplication;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateRequirement requirement)
        {
            var id = _httpContextAccessor.HttpContext.User?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (id == null) return;

            var userId = Guid.Parse(id);
            var controller = _httpContextAccessor.HttpContext.Request.RouteValues["controller"].ToString();

            var scopes = await _scopesApplication.GetScopes(userId, controller);

            var hasPermission = scopes.Crear;

            if (hasPermission)
            {
                _httpContextAccessor.HttpContext.Items["userId"] = userId;
                context.Succeed(requirement);
            }

            return;
        }
    }
}
