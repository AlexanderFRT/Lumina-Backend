namespace Lumina_Backend.Models;

public class BaseEntity
{
    public int Id { get; set; }

    public int Status { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public BaseEntity()
    {
        DateAdded = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        DateUpdated = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    }
}
