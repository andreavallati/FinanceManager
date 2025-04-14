using FinanceManager.Shared.Application.Requests;
using FinanceManager.Shared.Application.Responses;

namespace FinanceManager.WPF.Application.Interfaces.Services
{
    public interface IAuthUIService
    {
        Task<ApiResponseItem<TokenResponse>> LoginAsync(LoginRequest loginRequest);
    }
}