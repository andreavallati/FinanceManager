using FinanceManager.Shared.Enums;
using System.Windows.Input;

namespace FinanceManager.WPF.Presentation.Interfaces.ViewModels
{
    public interface IRegisterViewModel
    {
        ICommand RegisterCommand { get; }
        string Name { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        IEnumerable<string> UserRoles { get; }
        UserRole SelectedUserRole { get; set; }
        RegistrationStatus RegistrationStatus { get; set; }
        string RegistrationMessage { get; set; }
    }
}