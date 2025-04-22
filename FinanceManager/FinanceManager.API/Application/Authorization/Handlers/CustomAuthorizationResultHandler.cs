using FinanceManager.Shared.Application.Responses;
using FinanceManager.Shared.Constants;
using FinanceManager.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Text.Json;

namespace FinanceManager.API.Application.Authorization.Handlers
{
    public class CustomAuthorizationResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

        public async Task HandleAsync(RequestDelegate next,
                                      HttpContext context,
                                      AuthorizationPolicy policy,
                                      PolicyAuthorizationResult authorizeResult)
        {
            if (!authorizeResult.Succeeded)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status403Forbidden;

                var errorResponse = new ErrorResponse
                {
                    Message = Common.GenericAuthorizationMessage,
                    Errors = []
                };

                var responseJson = JsonSerializer.Serialize(errorResponse, JsonSerializerHelper.DefaultWriteOptions);

                await context.Response.WriteAsync(responseJson);
                return;
            }

            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
