namespace Lumina_Backend.Models;

public class Transaction : BaseEntity
{
    public string AccountNumber { get; set; }
    public virtual Account Account { get; set; }

    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public string? TransactionDescription { get; set; }

    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        Transfer
    }
}
