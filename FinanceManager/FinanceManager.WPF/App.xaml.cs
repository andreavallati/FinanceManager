using FinanceManager.WPF.Application.Mapping;
using FinanceManager.WPF.Extensions;
using FinanceManager.WPF.Presentation.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows;
using BaseApplication = System.Windows.Application;

namespace FinanceManager.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : BaseApplication
    {
        // The service provider that holds the DI container
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            // Configure DI services
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Build the service provider
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            // Register ILogger
            services.AddLogging(configure =>
            {
                configure.AddConsole();
                configure.AddDebug();
            });

            // Register AutoMapper and mapping profiles
            services.AddAutoMapper(typeof(FinanceManagerProfile));

            services.InjectRest();
            services.InjectValidators();
            services.InjectViewModels();
            services.InjectUIServices();
            services.InjectViews();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Resolve LoginView and show it
            var mainWindow = _serviceProvider.GetRequiredService<LoginView>();
            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}