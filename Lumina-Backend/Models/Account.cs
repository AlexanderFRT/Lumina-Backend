using System.ComponentModel.DataAnnotations.Schema;

namespace Lumina_Backend.Models;

public class Account : BaseEntity
{
    public virtual User User { get; set; }

    public int AccountNumber { get; set; }

    [Column(TypeName = "varchar(24)")]
    public AccountType Type { get; set; }
    public decimal Balance { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; }
}

public enum AccountType
{
    Savings,
    Checking
}