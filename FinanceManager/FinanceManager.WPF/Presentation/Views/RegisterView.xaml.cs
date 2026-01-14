using FinanceManager.WPF.Application.Interfaces.Validation;
using FinanceManager.WPF.Presentation.Interfaces.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FinanceManager.WPF.Presentation.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        public RegisterView(IRegisterViewModel registerViewModel)
        {
            DataContext = registerViewModel;
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is IRegisterViewModel vm && sender is PasswordBox pb)
            {
                vm.Password = pb.Password;

                if (vm is IValidatable validatable)
                {
                    validatable.ValidateProperty(nameof(vm.Password));

                    // Manually trigger validation visual feedback
                    var bindingExpression = BindingOperations.GetBindingExpression(pb, TagProperty);
                    bindingExpression?.UpdateTarget();
                }
            }
        }
    }
}