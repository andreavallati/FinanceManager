namespace FinanceManager.WPF.Application.Interfaces.Validation
{
    public interface IValidatable
    {
        void Validate();
        void ValidateProperty(string propertyName);
    }
}
