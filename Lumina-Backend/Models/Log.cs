namespace Lumina_Backend.Models;

public class Log : BaseEntity
{
    public virtual User User { get; set; }

    public ActionType Action { get; set; }

    /*
    public DateTime Timestamp { get; set; }

    public Log()
    {
        Timestamp = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    }
    */

    public enum ActionType
    {
        Login = 0,
        Logout = 1,
        Transaction = 2
    }
}
