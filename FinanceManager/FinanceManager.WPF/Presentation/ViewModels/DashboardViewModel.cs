using FinanceManager.Shared.Application.Authentication;
using FinanceManager.WPF.Presentation.Interfaces.Factory;
using FinanceManager.WPF.Presentation.Interfaces.ViewModels;
using FinanceManager.WPF.Presentation.Views;
using Microsoft.Extensions.Logging;
using Prism.Mvvm;
using System.Windows.Controls;

namespace FinanceManager.WPF.Presentation.ViewModels
{
    public class DashboardViewModel : BindableBase, IDashboardViewModel
    {
        private readonly IViewFactory _viewFactory;
        private readonly ILogger<DashboardViewModel> _logger;

        private UserControl _currentView;
        private string _welcomeMessage = string.Empty;

        public DashboardViewModel(IViewFactory viewFactory,
                                  ILogger<DashboardViewModel> logger)
        {
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            Init();
        }

        public UserControl CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set => SetProperty(ref _welcomeMessage, value);
        }

        private void Init()
        {
            try
            {
                _logger.LogInformation($"Init() called");

                if (!SessionManager.IsAuthenticated)
                {
                    _logger.LogInformation($"Init() message: User is not authenticated");

                    return;
                }

                WelcomeMessage = $"Welcome {SessionManager.Username}! You are logged as {SessionManager.Role}";

                if (SessionManager.IsAdmin)
                {
                    _logger.LogInformation($"Init() message: Admin user logged in");

                    var adminView = _viewFactory.CreateView<AdminView>();
                    CurrentView = adminView;
                }

                if (SessionManager.IsStandard)
                {
                    _logger.LogInformation($"Init() message: Standard user logged in");

                    var standardView = _viewFactory.CreateView<StandardView>();
                    CurrentView = standardView;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Init() exception: {{message}}", ex.Message);
            }
        }
    }
}
