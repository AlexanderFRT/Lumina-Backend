namespace Lumina_BackEnd.Models;

public class Account : BaseEntity
{
    public int UserID { get; set; }
    public User User { get; set; }

    public AccountType Type { get; set; }
    public decimal Balance { get; set; }
    public int AccountNumber { get; set; }

    public ICollection<Transaction> Transactions { get; set; }
}

public enum AccountType
{
    Checking,
    Savings
}
