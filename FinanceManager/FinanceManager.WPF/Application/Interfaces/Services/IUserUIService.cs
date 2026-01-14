using FinanceManager.Shared.Application.Responses;
using FinanceManager.WPF.Domain.Models;

namespace FinanceManager.WPF.Application.Interfaces.Services
{
    public interface IUserUIService
    {
        Task<ApiResponseItems<User>> GetAllAsync();
        Task<ApiResponseItem<User>> RegisterAsync(User user);
    }
}