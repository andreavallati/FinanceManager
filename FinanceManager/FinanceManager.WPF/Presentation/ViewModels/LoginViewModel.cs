using FinanceManager.Shared.Application.Authentication;
using FinanceManager.Shared.Application.Requests;
using FinanceManager.WPF.Application.Interfaces.Services.Factory;
using FinanceManager.WPF.Application.Services;
using FinanceManager.WPF.Presentation.Interfaces.ViewModels;
using FinanceManager.WPF.Presentation.ViewModels.Base;
using FinanceManager.WPF.Presentation.Views;
using FinanceManager.WPF.Resources;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using System.Windows;
using System.Windows.Input;
using BaseApplication = System.Windows.Application;

namespace FinanceManager.WPF.Presentation.ViewModels
{
    public class LoginViewModel : BaseViewModel<LoginViewModel>, ILoginViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<LoginViewModel> _logger;

        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isLoginFailed;

        public LoginViewModel(IServiceProvider serviceProvider,
                              IFactoryUIService factoryUIService,
                              IValidator<LoginViewModel> validator,
                              ILogger<LoginViewModel> logger) : base(factoryUIService, validator)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            LoginCommand = new DelegateCommand(async () => await LoginAsync(), CanLogin)
                .ObservesProperty(() => Email)
                .ObservesProperty(() => Password);

            RegisterCommand = new DelegateCommand(Register);
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public string Email
        {
            get => _email;
            set
            {
                if (SetProperty(ref _email, value))
                {
                    ValidateProperty(nameof(Email));
                }
            }
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsLoginFailed
        {
            get => _isLoginFailed;
            set => SetProperty(ref _isLoginFailed, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private async Task LoginAsync()
        {
            try
            {
                _logger.LogInformation($"LoginAsync() called");

                Validate();

                if (HasErrors)
                {
                    _logger.LogInformation($"LoginAsync() validation failed");

                    ErrorMessage = CommonResources.ValidationError;
                    IsLoginFailed = true;
                    return;
                }

                var loginRequest = new LoginRequest
                {
                    Email = Email,
                    Password = Password
                };

                var apiResponse = await _factoryUIService.CreateUIService<AuthUIService>()
                                                         .LoginAsync(loginRequest);

                if (!apiResponse.IsSuccess)
                {
                    ErrorMessage = apiResponse.ErrorMessage ?? string.Empty;
                    IsLoginFailed = true;

                    _logger.LogError($"LoginAsync() error: {{message}}", apiResponse.ErrorMessage);

                    return;
                }

                SessionManager.SetSession(apiResponse.Item?.Token ?? string.Empty,
                                          apiResponse.Item?.UserId ?? long.MinValue,
                                          apiResponse.Item?.Username ?? string.Empty,
                                          apiResponse.Item?.Role ?? Shared.Enums.UserRole.Standard);

                ErrorMessage = string.Empty;
                IsLoginFailed = false;

                var dashboardView = _serviceProvider.GetRequiredService<DashboardView>();
                dashboardView.Show();

                BaseApplication.Current.Windows.OfType<Window>()
                                               .FirstOrDefault(w => w is LoginView)?
                                               .Close();

                _logger.LogInformation($"LoginAsync() properly terminated");
            }
            catch (Exception ex)
            {
                ErrorMessage = CommonResources.GenericError;
                IsLoginFailed = true;

                _logger.LogError(ex, $"LoginAsync() exception: {{message}}", ex.Message);
            }
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }

        private void Register()
        {
            try
            {
                _logger.LogInformation($"Register() called");

                var registerView = _serviceProvider.GetRequiredService<RegisterView>();
                registerView.ShowDialog();

                _logger.LogInformation($"Register() properly terminated");
            }
            catch (Exception ex)
            {
                ErrorMessage = CommonResources.GenericError;

                _logger.LogError(ex, $"Register() exception: {{message}}", ex.Message);
            }
        }

        protected override LoginViewModel BuildValidationModel()
        {
            return this;
        }
    }
}