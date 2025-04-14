using FinanceManager.Shared.Constants;

namespace FinanceManager.Shared.Application.Dtos.Base
{
    public class BaseDto
    {
        public long Id { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.UtcNow.ToLocalTime();
        public string CreationUser { get; set; } = Common.CreationUsername;
        public DateTime? ModificationDate { get; set; }
        public string? ModificationUser { get; set; }
    }
}