using FinanceManager.API.Application.Interfaces.Repositories.Base;
using FinanceManager.API.Domain.Entities;

namespace FinanceManager.API.Application.Interfaces.Repositories
{
    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
        Task<IEnumerable<Transaction>> GetByUserIdAsync(long userId);
    }
}
