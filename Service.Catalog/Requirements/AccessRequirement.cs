﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Service.Catalog.Client.IClient;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.Catalog.Requirements
{
    public class AccessRequirement : IAuthorizationRequirement
    {
    }

    public class AccessRequirementHandler : AuthorizationHandler<AccessRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityClient _identityClient;

        public AccessRequirementHandler(IHttpContextAccessor httpContextAccessor, IIdentityClient identityClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityClient = identityClient;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AccessRequirement requirement)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext.User?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

            if (userId == Guid.Empty)
            {
                return;
            }

            var controller = _httpContextAccessor.HttpContext.Request.RouteValues["controller"].ToString();

            var scopes = await _identityClient.GetScopes(controller);

            var hasPermission = scopes.Acceder;

            if (hasPermission)
            {
                _httpContextAccessor.HttpContext.Items["userId"] = userId;
                context.Succeed(requirement);
            }

            return;
        }
    }
}
