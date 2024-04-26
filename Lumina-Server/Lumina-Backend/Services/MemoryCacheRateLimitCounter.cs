using Lumina_Backend.Options;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Lumina_Backend.Services;

public class MemoryCacheRateLimitCounter : IRateLimitCounter
{
    private readonly ConcurrentDictionary<string, RateLimitInfo> _requestCounts = new ConcurrentDictionary<string, RateLimitInfo>();
    private readonly int _limit;
    private readonly TimeSpan _window;

    public MemoryCacheRateLimitCounter(IOptions<RateLimitOptions> options)
    {
        _limit = options.Value.Limit;
        _window = options.Value.Window;
    }

    public bool IsAllowed(string clientId)
    {
        // Ensure that the RateLimitInfo object is initialized
        _requestCounts.TryGetValue(clientId, out RateLimitInfo? rateLimitInfo);

        // If rateLimitInfo is null, initialize it
        rateLimitInfo ??= new RateLimitInfo();

        // Check if the current request count exceeds the limit
        if (rateLimitInfo.Count >= _limit)
        {
            // Check if the time window has elapsed since the last reset
            if (DateTime.UtcNow - rateLimitInfo.LastReset >= _window)
            {
                // Reset the count if the window has elapsed
                rateLimitInfo.Count = 1;
                rateLimitInfo.LastReset = DateTime.UtcNow;
            }
            else
            {
                // If the window hasn't elapsed, the request is not allowed
                return false;
            }
        }
        else
        {
            // Increment the request count
            rateLimitInfo.Count++;
            // Update the last reset time if it's the first request in the window
            if (rateLimitInfo.Count == 1)
            {
                rateLimitInfo.LastReset = DateTime.UtcNow;
            }
        }

        return true;
    }

    public void Increment(string clientId)
    {
        // This method is called for every request, but the actual counting is done in IsAllowed method
        // No need to do anything here
    }

    // Helper class to store rate limit information per client
    private class RateLimitInfo
    {
        public int Count { get; set; }
        public DateTime LastReset { get; set; } = DateTime.UtcNow;
    }
}
