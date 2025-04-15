using FinanceManager.Shared.Enums;
using FinanceManager.WPF.Domain.Models.Base;

namespace FinanceManager.WPF.Domain.Models
{
    public class Transaction : BaseModel
    {
        public decimal Amount { get; set; } = decimal.Zero;
        public TransactionType Type { get; set; } = TransactionType.Income;
        public string? Category { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow.ToLocalTime();
        public string? Notes { get; set; } = string.Empty;

        public long UserId { get; set; }
    }
}
