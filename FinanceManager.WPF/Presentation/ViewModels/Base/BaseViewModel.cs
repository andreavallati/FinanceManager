using FinanceManager.Shared.Extensions;
using FinanceManager.WPF.Application.Interfaces.Services.Factory;
using FinanceManager.WPF.Application.Interfaces.Validation;
using FluentValidation;
using Prism.Mvvm;
using System.ComponentModel;

namespace FinanceManager.WPF.Presentation.ViewModels.Base
{
    public abstract class BaseViewModel<TValidationModel> : BindableBase, INotifyDataErrorInfo, IValidatable
        where TValidationModel : class
    {
        protected readonly IFactoryUIService _factoryUIService;
        protected readonly IValidator<TValidationModel> _validator;

        private readonly Dictionary<string, List<string>> _errors = [];

        private bool _validationCalled;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public bool HasErrors => _errors.Count != 0;

        protected BaseViewModel(IFactoryUIService uiService, IValidator<TValidationModel> validator)
        {
            _factoryUIService = uiService ?? throw new ArgumentNullException(nameof(uiService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public System.Collections.IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return Enumerable.Empty<string>();
            }

            return _errors.TryGetValue(propertyName, out var errors) ? errors : Enumerable.Empty<string>();
        }

        public void Validate()
        {
            _validationCalled = true;

            _errors.Clear();

            var model = BuildValidationModel();

            var result = _validator.Validate(model);
            foreach (var error in result.Errors)
            {
                if (!_errors.TryGetValue(error.PropertyName, out List<string>? value))
                {
                    value = ([]);
                    _errors[error.PropertyName] = value;
                }

                value.Add(error.ErrorMessage);
            }

            foreach (var propertyName in _errors.Keys)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }

            RaisePropertyChanged(nameof(HasErrors));
        }

        public void ValidateProperty(string propertyName)
        {
            if (!_validationCalled)
            {
                return;
            }

            var model = BuildValidationModel();

            var result = _validator.Validate(model);

            _errors.Remove(propertyName);

            var errors = result.Errors.Where(e => e.PropertyName == propertyName)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();

            if (errors.HasAny())
            {
                _errors[propertyName] = errors;
            }

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            RaisePropertyChanged(nameof(HasErrors));
        }

        // Each child ViewModel must implement this to provide the model to validate
        protected abstract TValidationModel BuildValidationModel();
    }
}