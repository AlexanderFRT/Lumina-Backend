using Lumina_Backend.Options;
using Lumina_Backend.Services;
using Microsoft.Extensions.Options;

namespace Lumina_Backend.Middleware;

public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly RateLimitOptions _options;
    private readonly IRateLimitCounter _rateLimitCounter;
    private readonly ILogger<RateLimitMiddleware> _logger; // Injected ILogger instance

    public RateLimitMiddleware(RequestDelegate next, IOptions<RateLimitOptions> options, IRateLimitCounter rateLimitCounter, ILogger<RateLimitMiddleware> logger)
    {
        _next = next;
        _options = options.Value;
        _rateLimitCounter = rateLimitCounter;
        _logger = logger; // Injected ILogger instance
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation("RateLimitMiddleware: Handling request...");

        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // Assuming the client's IP address is used as the identifier
        var clientId = context.Connection.RemoteIpAddress?.ToString(); // Null check added here
        if (clientId == null)
        {
            // Handle the case where the client IP address is not available
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Client IP address not available.");
            return;
        }

        if (_options == null)
        {
            _logger.LogError("RateLimitMiddleware: Rate limit options are not configured.");
            throw new InvalidOperationException("Rate limit options are not configured.");
        }

        if (!_rateLimitCounter.IsAllowed(clientId))
        {
            _logger.LogInformation("RateLimitMiddleware: Rate limit exceeded for client ID {ClientId}.", clientId);
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            await context.Response.WriteAsync("Rate limit exceeded.");
            return;
        }

        _rateLimitCounter.Increment(clientId);

        await _next(context);

        _logger.LogInformation("RateLimitMiddleware: Request handled successfully.");
    }
}