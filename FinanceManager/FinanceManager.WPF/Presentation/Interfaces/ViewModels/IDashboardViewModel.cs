using System.Windows.Controls;

namespace FinanceManager.WPF.Presentation.Interfaces.ViewModels
{
    public interface IDashboardViewModel
    {
        UserControl CurrentView { get; set; }
        string WelcomeMessage { get; set; }
    }
}
