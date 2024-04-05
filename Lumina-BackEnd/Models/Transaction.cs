namespace Lumina_Backend.Models;

public class Transaction : BaseEntity
{
    public int AccountID { get; set; }
    public Account Account { get; set; }

    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public string TransactionDescription { get; set; }
}
public enum TransactionType
{
    Deposit,
    Withdrawal,
    Transfer
}
