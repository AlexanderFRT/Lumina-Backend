using System.ComponentModel.DataAnnotations.Schema;

namespace Lumina_Backend.Models;

public class Transaction : BaseEntity
{
    public int AccountNumber { get; set; }
    public virtual Account? Account { get; set; }

    [Column(TypeName = "varchar(24)")]
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public string? TransactionDescription { get; set; }
}

public enum TransactionType
{
    Deposit,
    Withdrawal,
    Transfer
}
