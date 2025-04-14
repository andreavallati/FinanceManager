using FinanceManager.WPF.Application.Interfaces.Services.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.WPF.Application.Services.Factory
{
    public class FactoryUIService : IFactoryUIService
    {
        private readonly IServiceProvider _serviceProvider;

        public FactoryUIService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public TService CreateUIService<TService>() where TService : class
        {
            return _serviceProvider.GetRequiredService<TService>();
        }
    }
}