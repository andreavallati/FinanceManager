using RestSharp;

namespace FinanceManager.WPF.Application.Interfaces.Connectors
{
    public interface IRestManager
    {
        RestClient GetClient();
    }
}