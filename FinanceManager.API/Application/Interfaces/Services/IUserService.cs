using FinanceManager.Shared.Application.Dtos;

namespace FinanceManager.API.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> GetByLoginInfoAsync(string email, string password);
        Task<UserDto> InsertAsync(UserDto user);
    }
}