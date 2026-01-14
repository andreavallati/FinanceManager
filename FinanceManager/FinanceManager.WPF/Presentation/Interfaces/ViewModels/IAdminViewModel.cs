using FinanceManager.WPF.Domain.Models;
using System.Collections.ObjectModel;

namespace FinanceManager.WPF.Presentation.Interfaces.ViewModels
{
    public interface IAdminViewModel
    {
        ObservableCollection<User> Users { get; set; }
        string ErrorMessage { get; set; }
    }
}
