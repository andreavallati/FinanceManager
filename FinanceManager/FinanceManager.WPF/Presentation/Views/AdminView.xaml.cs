using FinanceManager.WPF.Presentation.Interfaces.ViewModels;
using FinanceManager.WPF.Presentation.Interfaces.Views;
using System.Windows.Controls;

namespace FinanceManager.WPF.Presentation.Views
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl, IFinanceView
    {
        public AdminView(IAdminViewModel adminViewModel)
        {
            DataContext = adminViewModel;
            InitializeComponent();
        }
    }
}
