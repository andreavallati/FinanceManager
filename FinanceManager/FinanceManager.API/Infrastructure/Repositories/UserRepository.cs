using FinanceManager.API.Application.Interfaces.Repositories;
using FinanceManager.API.Domain.Entities;
using FinanceManager.API.Infrastructure.Context;
using FinanceManager.API.Infrastructure.Repositories.Base;
using FinanceManager.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.API.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<User> GetByLoginInfoAsync(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => string.Equals(x.Email, email) && string.Equals(x.Password, password));
            if (user is null)
            {
                throw new RepositoryException("User is not registered in the system");
            }

            return user;
        }
    }
}