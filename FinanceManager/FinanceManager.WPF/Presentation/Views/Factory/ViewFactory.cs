using FinanceManager.WPF.Presentation.Interfaces.Factory;
using FinanceManager.WPF.Presentation.Interfaces.Views;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.WPF.Presentation.Views.Factory
{
    public class ViewFactory : IViewFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ViewFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public TView CreateView<TView>() where TView : IFinanceView
        {
            return _serviceProvider.GetRequiredService<TView>();
        }
    }
}
