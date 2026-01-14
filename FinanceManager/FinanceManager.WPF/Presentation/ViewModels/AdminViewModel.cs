using FinanceManager.Shared.Application.Authentication;
using FinanceManager.WPF.Application.Interfaces.Services.Factory;
using FinanceManager.WPF.Application.Services;
using FinanceManager.WPF.Domain.Models;
using FinanceManager.WPF.Presentation.Interfaces.ViewModels;
using FinanceManager.WPF.Presentation.ViewModels.Base;
using FinanceManager.WPF.Resources;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace FinanceManager.WPF.Presentation.ViewModels
{
    public class AdminViewModel : BaseViewModel<AdminViewModel>, IAdminViewModel
    {
        private readonly ILogger<AdminViewModel> _logger;

        private string _errorMessage = string.Empty;

        public AdminViewModel(IFactoryUIService factoryUIService,
                              IValidator<AdminViewModel> validator,
                              ILogger<AdminViewModel> logger) : base(factoryUIService, validator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _ = LoadUsersAsync();
        }

        public ObservableCollection<User> Users { get; set; } = [];

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                _logger.LogInformation($"LoadUsersAsync() called");

                Validate();

                if (HasErrors)
                {
                    _logger.LogInformation($"LoadUsersAsync() validation failed");

                    ErrorMessage = CommonResources.ValidationError;
                    return;
                }

                var apiResponse = await _factoryUIService.CreateUIService<UserUIService>()
                                                         .GetAllAsync();

                if (!apiResponse.IsSuccess)
                {
                    _logger.LogError($"LoadUsersAsync() error: {{message}}", apiResponse.ErrorMessage);

                    ErrorMessage = apiResponse.ErrorMessage ?? string.Empty;
                    return;
                }

                Users.AddRange(apiResponse.Items);

                // This should always fail since "StandardPolicy" authorization is required.
                var failingApiResponse = await _factoryUIService.CreateUIService<TransactionUIService>()
                                                                .GetByUserIdAsync(SessionManager.UserId);

                // Uncomment to test that "Unauthorized" error appears.
                //if (!failingApiResponse.IsSuccess)
                //{
                //    _logger.LogError($"LoadUsersAsync() error: {{message}}", failingApiResponse.ErrorMessage);

                //    ErrorMessage = failingApiResponse.ErrorMessage ?? string.Empty;
                //    return;
                //}

                _logger.LogInformation($"LoadUsersAsync() properly terminated");

                ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"LoadUsersAsync() exception: {{message}}", ex.Message);

                ErrorMessage = CommonResources.GenericError;
            }
        }

        protected override AdminViewModel BuildValidationModel()
        {
            return this;
        }
    }
}
