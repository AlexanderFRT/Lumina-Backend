namespace Lumina_Backend.Services;

public interface IRateLimitCounter
{
    bool IsAllowed(string clientId);
    void Increment(string clientId);
}
