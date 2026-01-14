using FinanceManager.Shared.Application.Dtos;
using FinanceManager.Shared.Application.Responses;

namespace FinanceManager.API.Application.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        TokenResponse GenerateToken(UserDto user);
    }
}