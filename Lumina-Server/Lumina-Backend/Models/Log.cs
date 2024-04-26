using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumina_Backend.Models;

public class Log
{
    [Key]
    public int Id { get; set; }
    public virtual User? User { get; set; }
    [Column(TypeName = "varchar(24)")]
    public ActionType Action { get; set; }
    [Column(TypeName = "varchar(24)")]
    public LogStatus Status { get; set; }
    public DateTime Timestamp { get; set; }

    public Log()
    {
        Timestamp = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    }
}

public enum ActionType
{
    Login,
    Logout,
    Transaction
}

public enum LogStatus
{
    Ok,
    Flagged       
}