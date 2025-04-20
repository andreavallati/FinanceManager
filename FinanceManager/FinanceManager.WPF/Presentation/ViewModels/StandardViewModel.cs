using FinanceManager.WPF.Domain.Models;
using FinanceManager.WPF.Presentation.Interfaces.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace FinanceManager.WPF.Presentation.ViewModels
{
    public class StandardViewModel : IStandardViewModel
    {
        private readonly ILogger<StandardViewModel> _logger;

        public StandardViewModel(ILogger<StandardViewModel> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ObservableCollection<Transaction> Transactions { get; set; } = [];
    }
}
