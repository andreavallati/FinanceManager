using System.Net;

namespace FinanceManager.Shared.Application.Responses.Base
{
    public class ApiResponseBase
    {
        public string? ErrorMessage { get; set; }
        public Exception? Exception { get; set; }
        public string? StackTrace { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; } = [];

        public bool IsSuccess => StatusCode == HttpStatusCode.OK || StatusCode == HttpStatusCode.Created;
    }
}