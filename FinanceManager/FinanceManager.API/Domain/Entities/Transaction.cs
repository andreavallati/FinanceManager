using FinanceManager.API.Domain.Entities.Base;
using FinanceManager.Shared.Enums;

namespace FinanceManager.API.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public decimal Amount { get; set; } = decimal.Zero;
        public TransactionType Type { get; set; } = TransactionType.Income;
        public string? Category { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow.ToLocalTime();
        public string? Notes { get; set; } = string.Empty;

        // FK & Navigation property to User
        public long UserId { get; set; }
        public User User { get; set; }
    }
}