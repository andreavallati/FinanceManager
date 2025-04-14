using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FinanceManager.Shared.Helpers
{
    public static class JsonSerializerHelper
    {
        public static readonly string JsonContentType = "application/json";

        public static readonly JsonSerializerOptions DefaultReadOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
        };

        public static readonly JsonSerializerOptions DefaultWriteOptions = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public static readonly JsonSerializerOptions IgnoreNullWriteOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
    }
}