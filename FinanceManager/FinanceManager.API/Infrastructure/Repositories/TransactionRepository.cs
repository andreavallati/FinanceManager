using FinanceManager.API.Application.Interfaces.Repositories;
using FinanceManager.API.Domain.Entities;
using FinanceManager.API.Infrastructure.Context;
using FinanceManager.API.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.API.Infrastructure.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(long userId)
        {
            var transactions = await _context.Transactions.Where(x => x.UserId == userId)
                                                          .ToListAsync();
            return transactions;
        }
    }
}
