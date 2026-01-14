using FinanceManager.WPF.Presentation.Interfaces.Views;

namespace FinanceManager.WPF.Presentation.Interfaces.Factory
{
    public interface IViewFactory
    {
        TView CreateView<TView>() where TView : IFinanceView;
    }
}
