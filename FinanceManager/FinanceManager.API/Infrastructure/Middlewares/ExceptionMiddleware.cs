using FinanceManager.Shared.Application.Responses;
using FinanceManager.Shared.Exceptions;
using FinanceManager.Shared.Helpers;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Text.Json;

namespace FinanceManager.API.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (AuthorizationException ex)
            {
                _logger.LogError(ex, $"Authorization exception: {{message}}", ex.Message);

                await HandleAuthorizationExceptionAsync(httpContext, ex);
            }
            catch (ValidatorException ex)
            {
                // FluentValidation errors
                _logger.LogError(ex, $"Validation exception: \n{{message}}", string.Join("\n", ex.ValidationErrors));

                await HandleValidationExceptionAsync(httpContext, ex);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Service exception: {{message}}", ex.Message);

                await HandleServiceExceptionAsync(httpContext, ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(ex, $"Repository exception: {{message}}", ex.Message);

                await HandleRepositoryExceptionAsync(httpContext, ex);
            }
            catch (TechnicalException ex)
            {
                _logger.LogError(ex, $"Technical exception: {{message}}", ex.Message);

                await HandleTechnicalExceptionAsync(httpContext, ex);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"SQL exception: {{message}}", ex.Message);

                await HandleExceptionAsync(httpContext, "Database generic error", [], HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Generic exception: {{message}}", ex.Message);

                await HandleExceptionAsync(httpContext, ex.Message, [], HttpStatusCode.InternalServerError);
            }
        }

        private static Task HandleAuthorizationExceptionAsync(HttpContext context, AuthorizationException exception)
        {
            return HandleExceptionAsync(context, exception.Message, [], HttpStatusCode.Unauthorized);
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidatorException exception)
        {
            return HandleExceptionAsync(context, exception.Message, exception.ValidationErrors, HttpStatusCode.BadRequest);
        }

        private static Task HandleServiceExceptionAsync(HttpContext context, ServiceException exception)
        {
            return HandleExceptionAsync(context, exception.Message, [], HttpStatusCode.BadRequest);
        }

        private static Task HandleRepositoryExceptionAsync(HttpContext context, RepositoryException exception)
        {
            return HandleExceptionAsync(context, exception.Message, [], HttpStatusCode.BadRequest);
        }

        private static Task HandleTechnicalExceptionAsync(HttpContext context, TechnicalException exception)
        {
            return HandleExceptionAsync(context, exception.Message, [], HttpStatusCode.BadRequest);
        }

        private static Task HandleExceptionAsync(HttpContext context,
                                                 string message,
                                                 IEnumerable<string> errors,
                                                 HttpStatusCode statusCode)
        {
            context.Response.ContentType = JsonSerializerHelper.JsonContentType;
            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new ErrorResponse
            {
                Message = message,
                Errors = errors
            };

            var responseJson = JsonSerializer.Serialize(errorResponse, JsonSerializerHelper.DefaultWriteOptions);
            return context.Response.WriteAsync(responseJson);
        }
    }
}