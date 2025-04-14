using FinanceManager.Shared.Constants;

namespace FinanceManager.WPF.Domain.Models.Base
{
    public class BaseModel
    {
        public long Id { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.UtcNow.ToLocalTime();
        public string CreationUser { get; set; } = Common.CreationUsername;
        public DateTime? ModificationDate { get; set; }
        public string? ModificationUser { get; set; }
    }
}