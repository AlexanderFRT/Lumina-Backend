namespace Lumina_Backend.Models;

public class Security : BaseEntity
{
    public int UserId { get; set; }
    public virtual User User { get; set; }

    public string SecurityQuestions { get; set; }
    public string SecurityAnswers { get; set; }
}
