using System.Windows.Input;

namespace FinanceManager.WPF.Presentation.Interfaces.ViewModels
{
    public interface ILoginViewModel
    {
        ICommand LoginCommand { get; }
        ICommand RegisterCommand { get; }
        string Email { get; set; }
        string Password { get; set; }
        bool IsLoginFailed { get; set; }
        string ErrorMessage { get; set; }
    }
}