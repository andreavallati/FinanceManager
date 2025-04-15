using FinanceManager.Shared.Application.Dtos.Base;
using FinanceManager.Shared.Enums;

namespace FinanceManager.Shared.Application.Dtos
{
    public class TransactionDto : BaseDto
    {
        public decimal Amount { get; set; } = decimal.Zero;
        public TransactionType Type { get; set; } = TransactionType.Income;
        public string? Category { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow.ToLocalTime();
        public string? Notes { get; set; } = string.Empty;

        public long UserId { get; set; }
    }
}
