namespace Lumina_BackEnd.Models;

public class Security : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public string SecurityQuestions { get; set; }
    public string SecurityAnswers { get; set; }
}
