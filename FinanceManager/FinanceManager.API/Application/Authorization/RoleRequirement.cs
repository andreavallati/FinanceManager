using Microsoft.AspNetCore.Authorization;

namespace FinanceManager.API.Application.Authorization
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string RequiredRole { get; }
        public RoleRequirement(string role) => RequiredRole = role;
    }
}