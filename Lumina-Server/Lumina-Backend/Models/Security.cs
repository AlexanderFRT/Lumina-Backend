namespace Lumina_Backend.Models;

public class Security : BaseEntity
{
    public virtual User User { get; set; }

    public string SecurityQuestion { get; set; }
    public string SecurityAnswer { get; set; }
}
