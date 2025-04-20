using FinanceManager.WPF.Presentation.Interfaces.ViewModels;
using FinanceManager.WPF.Presentation.Interfaces.Views;
using System.Windows.Controls;

namespace FinanceManager.WPF.Presentation.Views
{
    /// <summary>
    /// Interaction logic for StandardView.xaml
    /// </summary>
    public partial class StandardView : UserControl, IFinanceView
    {
        public StandardView(IStandardViewModel standardViewModel)
        {
            DataContext = standardViewModel;
            InitializeComponent();
        }
    }
}
