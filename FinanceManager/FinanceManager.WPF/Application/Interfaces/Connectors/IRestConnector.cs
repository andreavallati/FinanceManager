using FinanceManager.Shared.Application.Responses;

namespace FinanceManager.WPF.Application.Interfaces.Connectors
{
    public interface IRestConnector
    {
        Task<ApiResponseItem<TResult>> GetModelAsync<TResult>(string endpoint);
        Task<ApiResponseItems<TResult>> GetModelsAsync<TResult>(string endpoint);
        Task<ApiResponseItem<TResult>> PostModelAsync<TResult, TRequest>(string endpoint, TRequest request);
        Task<ApiResponseItems<TResult>> PostModelsAsync<TResult, TRequest>(string endpoint, TRequest request);
        Task<ApiResponseItem<TResult>> PutModelAsync<TResult, TRequest>(string endpoint, TRequest request);
        Task<ApiResponseItems<TResult>> PutModelsAsync<TResult, TRequest>(string endpoint, TRequest request);
        Task<ApiResponseItem<TResult>> DeleteModelAsync<TResult>(string endpoint);
        Task<ApiResponseItems<TResult>> DeleteModelsAsync<TResult>(string endpoint);
    }
}