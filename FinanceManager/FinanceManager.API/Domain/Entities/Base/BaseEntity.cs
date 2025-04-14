using FinanceManager.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.API.Domain.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.UtcNow.ToLocalTime();
        public string CreationUser { get; set; } = Common.CreationUsername;
        public DateTime? ModificationDate { get; set; }
        public string? ModificationUser { get; set; }
    }
}