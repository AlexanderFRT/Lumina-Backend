namespace Lumina_Backend.Models;

public class Account : BaseEntity
{
    public virtual User User { get; set; }

    public string AccountNumber { get; set; }
    public AccountType Type { get; set; }
    public decimal Balance { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; }

    public enum AccountType
    {
        Checking,
        Savings
    }
}
