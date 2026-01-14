using FinanceManager.Shared.Enums;
using FinanceManager.WPF.Domain.Models.Base;

namespace FinanceManager.WPF.Domain.Models
{
    public class User : BaseModel
    {
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Standard;

        public ICollection<Transaction> Transactions { get; set; } = [];
    }
}
