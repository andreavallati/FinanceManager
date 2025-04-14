using FinanceManager.API.Application.Interfaces.Repositories.Base;
using FinanceManager.API.Domain.Entities;

namespace FinanceManager.API.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetByLoginInfoAsync(string email, string password);
    }
}