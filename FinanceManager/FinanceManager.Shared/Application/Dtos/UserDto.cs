using FinanceManager.Shared.Application.Dtos.Base;
using FinanceManager.Shared.Enums;

namespace FinanceManager.Shared.Application.Dtos
{
    public class UserDto : BaseDto
    {
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Standard;

        public ICollection<TransactionDto> Transactions { get; set; } = [];
    }
}