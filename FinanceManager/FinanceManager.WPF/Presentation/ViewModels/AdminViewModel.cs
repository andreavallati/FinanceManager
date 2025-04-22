using FinanceManager.WPF.Application.Interfaces.Services.Factory;
using FinanceManager.WPF.Application.Services;
using FinanceManager.WPF.Domain.Models;
using FinanceManager.WPF.Presentation.Interfaces.ViewModels;
using FinanceManager.WPF.Presentation.ViewModels.Base;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace FinanceManager.WPF.Presentation.ViewModels
{
    public class AdminViewModel : BaseViewModel<AdminViewModel>, IAdminViewModel
    {
        private readonly ILogger<AdminViewModel> _logger;

        public AdminViewModel(IFactoryUIService factoryUIService,
                              IValidator<AdminViewModel> validator,
                              ILogger<AdminViewModel> logger) : base(factoryUIService, validator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _ = LoadUsersAsync();
        }

        public ObservableCollection<User> Users { get; set; } = [];

        private async Task LoadUsersAsync()
        {
            try
            {
                _logger.LogInformation($"LoadUsersAsync() called");

                Validate();

                if (HasErrors)
                {
                    _logger.LogInformation($"LoadUsersAsync() validation failed");

                    return;
                }

                var apiResponse = await _factoryUIService.CreateUIService<UserUIService>()
                                                         .GetUsersAsync();

                if (!apiResponse.IsSuccess)
                {
                    _logger.LogError($"LoadUsersAsync() error: {{message}}", apiResponse.ErrorMessage);

                    return;
                }

                Users.AddRange(apiResponse.Items);

                _logger.LogInformation($"LoadUsersAsync() properly terminated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"LoadUsersAsync() exception: {{message}}", ex.Message);
            }
        }

        protected override AdminViewModel BuildValidationModel()
        {
            return this;
        }
    }
}
