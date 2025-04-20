using FinanceManager.WPF.Domain.Models;
using FinanceManager.WPF.Presentation.Interfaces.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace FinanceManager.WPF.Presentation.ViewModels
{
    public class AdminViewModel : IAdminViewModel
    {
        private readonly ILogger<AdminViewModel> _logger;

        public AdminViewModel(ILogger<AdminViewModel> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ObservableCollection<User> Users { get; set; } = [];
    }
}
