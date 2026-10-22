namespace CampusEvents.Api.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string API_KEY_HEADER_NAME = "X-API-KEY";
    private const string ApiKeyConfigKey = "Apisettings:ApiKey";
    private readonly string _apikey;
    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _apikey = configuration[ApiKeyConfigKey]
        ?? throw new InvalidCastException("API Key is not configured.set 'ApiKeyConfigKey' in appsettings");
    }
    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Unauthorized
            await context.Response.WriteAsJsonAsync(new { error = "API Key is missing" });
            return;
        }

        if (!string.Equals(extractedApiKey, _apikey, StringComparison.Ordinal))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Unauthorized
            await context.Response.WriteAsJsonAsync(new { error = "Invalid API Key" });
            return;
        }



        await _next(context);

    }
}