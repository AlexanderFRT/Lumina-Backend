namespace Lumina_Backend.Options;

public class RateLimitOptions
{
    // Límite de requests por ventana de tiempo, si se excede sucederá un timeout
    public int Limit { get; set; }
    // Ventana de tiempo en la que se cuenta el número de requests (1 minuto)
    public TimeSpan Window { get; set; }
}
