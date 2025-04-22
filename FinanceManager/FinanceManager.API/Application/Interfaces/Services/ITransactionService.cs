using FinanceManager.Shared.Application.Dtos;

namespace FinanceManager.API.Application.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetByUserIdAsync(long userId);
    }
}
