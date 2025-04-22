using FinanceManager.Shared.Application.Authentication;
using FinanceManager.Shared.Application.Responses;
using FinanceManager.Shared.Helpers;
using FinanceManager.WPF.Application.Interfaces.Connectors;
using RestSharp;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace FinanceManager.WPF.Infrastructure.Connectors
{
    public class RestConnector : IRestConnector
    {
        private readonly RestClient _restClient;

        public RestConnector(IRestManager restClientManager)
        {
            _restClient = restClientManager.GetClient();
        }

        public async Task<ApiResponseItem<TResult>> GetModelAsync<TResult>(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Get);
            IncludeAuthorization(request);

            var response = await _restClient.ExecuteAsync(request);
            return HandleHttpItemResponse<TResult>(response);
        }

        public async Task<ApiResponseItems<TResult>> GetModelsAsync<TResult>(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Get);
            IncludeAuthorization(request);

            var response = await _restClient.ExecuteAsync(request);
            return HandleHttpItemsResponse<TResult>(response);
        }

        public async Task<ApiResponseItem<TResult>> PostModelAsync<TResult, TRequest>(string endpoint, TRequest request)
        {
            ArgumentNullException.ThrowIfNull(nameof(request));

            var requestObj = BuildJsonRequest(endpoint, Method.Post, request);
            var response = await _restClient.ExecuteAsync(requestObj);
            return HandleHttpItemResponse<TResult>(response);
        }

        public async Task<ApiResponseItems<TResult>> PostModelsAsync<TResult, TRequest>(string endpoint, TRequest request)
        {
            ArgumentNullException.ThrowIfNull(nameof(request));

            var requestObj = BuildJsonRequest(endpoint, Method.Post, request);
            var response = await _restClient.ExecuteAsync(requestObj);
            return HandleHttpItemsResponse<TResult>(response);
        }

        public async Task<ApiResponseItem<TResult>> PutModelAsync<TResult, TRequest>(string endpoint, TRequest request)
        {
            ArgumentNullException.ThrowIfNull(nameof(request));

            var requestObj = BuildJsonRequest(endpoint, Method.Put, request);
            var response = await _restClient.ExecuteAsync(requestObj);
            return HandleHttpItemResponse<TResult>(response);
        }

        public async Task<ApiResponseItems<TResult>> PutModelsAsync<TResult, TRequest>(string endpoint, TRequest request)
        {
            ArgumentNullException.ThrowIfNull(nameof(request));

            var requestObj = BuildJsonRequest(endpoint, Method.Put, request);
            var response = await _restClient.ExecuteAsync(requestObj);
            return HandleHttpItemsResponse<TResult>(response);
        }

        public async Task<ApiResponseItem<TResult>> DeleteModelAsync<TResult>(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Delete);
            IncludeAuthorization(request);

            var response = await _restClient.ExecuteAsync(request);
            return HandleHttpItemResponse<TResult>(response);
        }

        public async Task<ApiResponseItems<TResult>> DeleteModelsAsync<TResult>(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Delete);
            IncludeAuthorization(request);

            var response = await _restClient.ExecuteAsync(request);
            return HandleHttpItemsResponse<TResult>(response);
        }

        private static RestRequest BuildJsonRequest<TRequest>(string endpoint, Method method, TRequest body)
        {
            var jsonBody = JsonSerializer.Serialize(body, JsonSerializerHelper.IgnoreNullWriteOptions);
            var request = new RestRequest(endpoint, method);
            request.AddJsonBody(jsonBody);

            IncludeAuthorization(request);

            return request;
        }

        private static void IncludeAuthorization(RestRequest request)
        {
            // Add Authorization if user is authenticated
            if (SessionManager.IsAuthenticated)
            {
                request.AddHeader("Authorization", $"Bearer {SessionManager.Token}");
            }
        }

        private static ApiResponseItem<TResult> HandleHttpItemResponse<TResult>(RestResponse response)
        {
            try
            {
                if (response.IsSuccessful)
                {
                    if (string.IsNullOrWhiteSpace(response.Content))
                    {
                        return ApiResponseItem<TResult>.Success(default!, response.StatusCode);
                    }

                    var result = JsonSerializer.Deserialize<TResult>(response.Content!, JsonSerializerHelper.DefaultReadOptions) ?? default!;
                    return ApiResponseItem<TResult>.Success(result, response.StatusCode);
                }

                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(response.Content!, JsonSerializerHelper.DefaultReadOptions) ?? default!;
                return ApiResponseItem<TResult>.Failure(errorResponse.Message, response.StatusCode, errorResponse.Errors);
            }
            catch (HttpRequestException ex)
            {
                return ApiResponseItem<TResult>.Failure(ex.Message, ex, ex.StackTrace, ex.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponseItem<TResult>.Failure(ex.Message, ex, ex.StackTrace, HttpStatusCode.InternalServerError);
            }
        }

        private static ApiResponseItems<TResult> HandleHttpItemsResponse<TResult>(RestResponse response)
        {
            try
            {
                if (response.IsSuccessful)
                {
                    if (string.IsNullOrWhiteSpace(response.Content))
                    {
                        return ApiResponseItems<TResult>.Success([], response.StatusCode);
                    }

                    var result = JsonSerializer.Deserialize<IEnumerable<TResult>>(response.Content!, JsonSerializerHelper.DefaultReadOptions) ?? default!;
                    return ApiResponseItems<TResult>.Success(result, response.StatusCode);
                }

                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(response.Content!, JsonSerializerHelper.DefaultReadOptions) ?? default!;
                return ApiResponseItems<TResult>.Failure(errorResponse.Message, response.StatusCode, errorResponse.Errors);
            }
            catch (HttpRequestException ex)
            {
                return ApiResponseItems<TResult>.Failure(ex.Message, ex, ex.StackTrace, ex.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponseItems<TResult>.Failure(ex.Message, ex, ex.StackTrace, HttpStatusCode.InternalServerError);
            }
        }
    }
}