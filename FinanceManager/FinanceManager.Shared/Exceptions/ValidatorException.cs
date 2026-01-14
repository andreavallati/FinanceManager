using FinanceManager.Shared.Constants;

namespace FinanceManager.Shared.Exceptions
{
    public class ValidatorException : Exception
    {
        public IEnumerable<string> ValidationErrors { get; }

        public ValidatorException(IEnumerable<string> validationErrors) : base(Common.GenericValidationMessage)
        {
            ValidationErrors = validationErrors;
        }
    }
}
