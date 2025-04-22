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
    public class StandardViewModel : BaseViewModel<StandardViewModel>, IStandardViewModel
    {
        private readonly ILogger<StandardViewModel> _logger;

        private string _errorMessage = string.Empty;

        public StandardViewModel(IFactoryUIService factoryUIService,
                                 IValidator<StandardViewModel> validator,
                                 ILogger<StandardViewModel> logger) : base(factoryUIService, validator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _ = LoadTransactionsAsync();
        }

        public ObservableCollection<Transaction> Transactions { get; set; } = [];

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private async Task LoadTransactionsAsync()
        {
            try
            {
                _logger.LogInformation($"LoadTransactionsAsync() called");

                Validate();

                if (HasErrors)
                {
                    _logger.LogInformation($"LoadTransactionsAsync() validation failed");

                    ErrorMessage = CommonResources.ValidationError;
                    return;
                }

                var apiResponse = await _factoryUIService.CreateUIService<TransactionUIService>()
                                                         .GetByUserIdAsync(SessionManager.UserId);

                if (!apiResponse.IsSuccess)
                {
                    _logger.LogError($"LoadTransactionsAsync() error: {{message}}", apiResponse.ErrorMessage);

                    ErrorMessage = apiResponse.ErrorMessage ?? string.Empty;
                    return;
                }

                Transactions.AddRange(apiResponse.Items);

                // This should always fail since "AdminPolicy" authorization is required.
                var failingApiResponse = await _factoryUIService.CreateUIService<UserUIService>()
                                                                .GetAllAsync();

                // Uncomment to test that "Unauthorized" error appears.
                //if (!failingApiResponse.IsSuccess)
                //{
                //    _logger.LogError($"LoadTransactionsAsync() error: {{message}}", failingApiResponse.ErrorMessage);

                //    ErrorMessage = failingApiResponse.ErrorMessage ?? string.Empty;
                //    return;
                //}

                _logger.LogInformation($"LoadTransactionsAsync() properly terminated");

                ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"LoadTransactionsAsync() exception: {{message}}", ex.Message);

                ErrorMessage = CommonResources.GenericError;
            }
        }

        protected override StandardViewModel BuildValidationModel()
        {
            return this;
        }
    }
}
