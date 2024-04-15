namespace Lumina_Backend.Models;

public class BaseEntity
{
    public int Id { get; set; }

    public EntityStatus Status { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public BaseEntity()
    {
        if (DateAdded == DateTime.MinValue)
        {
            DateAdded = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        }

        DateUpdated = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    }

    public enum EntityStatus
    {
        Unverified,
        Verified,
        Active,
        Inactive,
        Frozen,
        Closed,
        Pending,
        Completed,
        Failed,
        Reversed,
        PendingReview,
        Approved,
        Rejected,
        Flagged,
        Ok
    }
}
