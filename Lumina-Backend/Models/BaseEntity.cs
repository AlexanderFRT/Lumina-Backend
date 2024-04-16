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
        Unverified = 0,
        Verified = 1,
        Active = 2,
        Inactive = 3,
        Frozen = 4,
        Closed = 5,
        Pending = 6,
        Completed = 7,
        Failed = 8,
        Reversed = 9,
        PendingReview = 10,
        Approved = 11,
        Rejected = 12,
        Flagged = 13,
        Ok = 14
    }
}
