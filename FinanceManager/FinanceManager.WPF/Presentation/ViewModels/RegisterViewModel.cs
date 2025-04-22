using FinanceManager.Shared.Constants;
using FinanceManager.Shared.Enums;
using FinanceManager.Shared.Extensions;
using FinanceManager.WPF.Application.Interfaces.Services.Factory;
using FinanceManager.WPF.Application.Services;
using FinanceManager.WPF.Domain.Models;
using FinanceManager.WPF.Presentation.Interfaces.ViewModels;
using FinanceManager.WPF.Presentation.ViewModels.Base;
using FinanceManager.WPF.Presentation.Views;
using FinanceManager.WPF.Resources;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using System.Windows;
using System.Windows.Input;
using BaseApplication = System.Windows.Application;

namespace FinanceManager.WPF.Presentation.ViewModels
{
    public class RegisterViewModel : BaseViewModel<RegisterViewModel>, IRegisterViewModel
    {
        private readonly ILogger<RegisterViewModel> _logger;

        private string _name = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private UserRole _selectedRole;
        private RegistrationStatus _registrationStatus;
        private string _registrationMessage = string.Empty;

        public RegisterViewModel(IFactoryUIService factoryUIService,
                                 IValidator<RegisterViewModel> validator,
                                 ILogger<RegisterViewModel> logger) : base(factoryUIService, validator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            RegisterCommand = new DelegateCommand(async () => await RegisterAsync(), CanRegister)
                .ObservesProperty(() => Name)
                .ObservesProperty(() => Email)
                .ObservesProperty(() => Password)
                .ObservesProperty(() => SelectedUserRole);
        }

        public ICommand RegisterCommand { get; }

        public string Name
        {
            get => _name;
            set
            {
                if (SetProperty(ref _name, value))
                {
                    ValidateProperty(nameof(Name));
                }
            }
        }

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

        public IEnumerable<string> UserRoles
        {
            get
            {
                foreach (Enum enumValue in Enum.GetValues(typeof(UserRole)))
                {
                    yield return EnumExtensions.GetDescription(enumValue);
                }
            }
        }

        public UserRole SelectedUserRole
        {
            get => _selectedRole;
            set
            {
                if (SetProperty(ref _selectedRole, value))
                {
                    ValidateProperty(nameof(SelectedUserRole));
                }
            }
        }

        public RegistrationStatus RegistrationStatus
        {
            get => _registrationStatus;
            set => SetProperty(ref _registrationStatus, value);
        }

        public string RegistrationMessage
        {
            get => _registrationMessage;
            set => SetProperty(ref _registrationMessage, value);
        }

        private async Task RegisterAsync()
        {
            try
            {
                _logger.LogInformation($"RegisterAsync() called");

                Validate();

                if (HasErrors)
                {
                    _logger.LogInformation($"RegisterAsync() validation failed");

                    RegistrationMessage = CommonResources.ValidationError;
                    RegistrationStatus = RegistrationStatus.Failure;
                    return;
                }

                var user = new User
                {
                    Name = Name,
                    Email = Email,
                    Password = Password,
                    Role = SelectedUserRole,
                    Transactions = new List<Transaction>
                    {
                        new Transaction
                        {
                            Amount = 1750.00m,
                            Type = TransactionType.Income,
                            Category = "Salary",
                            Date = DateTime.UtcNow.AddDays(-7),
                            Notes = "Monthly paycheck"
                        },
                        new Transaction
                        {
                            Amount = 75.50m,
                            Type = TransactionType.Expense,
                            Category = "Groceries",
                            Date = DateTime.UtcNow.AddDays(-3),
                            Notes = "Weekly groceries"
                        }
                    }
                };

                var apiResponse = await _factoryUIService.CreateUIService<UserUIService>()
                                                         .RegisterAsync(user);

                if (!apiResponse.IsSuccess)
                {
                    _logger.LogError($"RegisterAsync() error: {{message}}", apiResponse.ErrorMessage);

                    RegistrationMessage = apiResponse.ErrorMessage ?? string.Empty;
                    RegistrationStatus = RegistrationStatus.Failure;

                    return;
                }

                _logger.LogInformation($"RegisterAsync() properly terminated");

                RegistrationMessage = CommonResources.RegistrationSuccess;
                RegistrationStatus = RegistrationStatus.Success;

                await Task.Delay(Common.WindowClosingDelay);

                BaseApplication.Current.Windows.OfType<Window>()
                                               .FirstOrDefault(w => w is RegisterView)?
                                               .Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"RegisterAsync() exception: {{message}}", ex.Message);

                RegistrationMessage = CommonResources.GenericError;
                RegistrationStatus = RegistrationStatus.Failure;
            }
        }

        private bool CanRegister()
        {
            return !string.IsNullOrEmpty(Name) &&
                   !string.IsNullOrEmpty(Email) &&
                   !string.IsNullOrEmpty(Password) &&
                   SelectedUserRole != default;
        }

        protected override RegisterViewModel BuildValidationModel()
        {
            return this;
        }
    }
}