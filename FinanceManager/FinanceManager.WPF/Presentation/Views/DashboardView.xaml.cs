using FinanceManager.WPF.Presentation.Interfaces.ViewModels;
using System.Windows;

namespace FinanceManager.WPF.Presentation.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Window
    {
        public DashboardView(IDashboardViewModel dashboardViewModel)
        {
            DataContext = dashboardViewModel;
            InitializeComponent();
        }
    }
}
