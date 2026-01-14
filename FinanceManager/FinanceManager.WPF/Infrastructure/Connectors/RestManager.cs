using FinanceManager.Shared.Constants;
using FinanceManager.WPF.Application.Interfaces.Connectors;
using RestSharp;

namespace FinanceManager.WPF.Infrastructure.Connectors
{
    public class RestManager : IRestManager
    {
        private readonly RestClient _client;

        public RestManager()
        {
            var baseUrl = Common.ApiEndpoint;
            _client = new RestClient(baseUrl);
        }

        public RestClient GetClient()
        {
            return _client;
        }
    }
}