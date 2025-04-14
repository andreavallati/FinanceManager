using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FinanceManager.API.Application.Authorization.Handlers
{
    public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var roleClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (roleClaim == requirement.RequiredRole)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}