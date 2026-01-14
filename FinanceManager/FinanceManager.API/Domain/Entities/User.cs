using FinanceManager.API.Domain.Entities.Base;
using FinanceManager.Shared.Enums;

namespace FinanceManager.API.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Standard;

        // Navigation property
        public ICollection<Transaction> Transactions { get; set; } = [];
    }
}