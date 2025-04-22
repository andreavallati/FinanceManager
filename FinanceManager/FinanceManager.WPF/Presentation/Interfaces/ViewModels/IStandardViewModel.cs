using FinanceManager.WPF.Domain.Models;
using System.Collections.ObjectModel;

namespace FinanceManager.WPF.Presentation.Interfaces.ViewModels
{
    public interface IStandardViewModel
    {
        ObservableCollection<Transaction> Transactions { get; set; }
        string ErrorMessage { get; set; }
    }
}
