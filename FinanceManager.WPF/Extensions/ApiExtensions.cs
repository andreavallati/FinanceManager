using AutoMapper;
using FinanceManager.Shared.Application.Responses;

namespace FinanceManager.WPF.Extensions
{
    public static class ApiExtensions
    {
        public static async Task<ApiResponseItem<TResult>> ProcessItemResponse<TResult, TSource>(this Func<Task<ApiResponseItem<TSource>>> apiCall,
                                                                                                 IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(apiCall);
            ArgumentNullException.ThrowIfNull(mapper);

            try
            {
                var response = await apiCall();

                if (response.IsSuccess)
                {
                    var result = mapper.Map<TResult>(response.Item);
                    return ApiResponseItem<TResult>.Success(result, response.StatusCode);
                }

                return ApiResponseItem<TResult>.Failure(response.ErrorMessage, response.StatusCode, response.ValidationErrors);
            }
            catch (Exception ex)
            {
                return ApiResponseItem<TResult>.Failure(ex, ex.StackTrace);
            }
        }

        public static async Task<ApiResponseItems<TResult>> ProcessItemsResponse<TResult, TSource>(this Func<Task<ApiResponseItems<TSource>>> apiCall,
                                                                                                   IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(apiCall);
            ArgumentNullException.ThrowIfNull(mapper);

            try
            {
                var response = await apiCall();

                if (response.IsSuccess)
                {
                    var result = mapper.Map<IEnumerable<TResult>>(response.Items);
                    return ApiResponseItems<TResult>.Success(result, response.StatusCode);
                }

                return ApiResponseItems<TResult>.Failure(response.ErrorMessage, response.StatusCode, response.ValidationErrors);
            }
            catch (Exception ex)
            {
                return ApiResponseItems<TResult>.Failure(ex, ex.StackTrace);
            }
        }
    }
}