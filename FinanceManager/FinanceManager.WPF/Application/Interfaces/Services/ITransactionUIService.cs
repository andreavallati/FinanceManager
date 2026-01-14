using FinanceManager.Shared.Application.Responses;
using FinanceManager.WPF.Domain.Models;

namespace FinanceManager.WPF.Application.Interfaces.Services
{
    public interface ITransactionUIService
    {
        Task<ApiResponseItems<Transaction>> GetByUserIdAsync(long userId);
    }
}
