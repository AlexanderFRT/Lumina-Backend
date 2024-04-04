using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lumina_BackEnd.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        public int accountId { get; }

        public TransactionType transaction{ get; set; }

        public enum TransactionType
        {
            Deposit,
            Withdrawal,
            Transfer,
        }

        public decimal amount { get; set; }
        public DateTime timeOfTransaction { get; set; }

        public string description { get; set; }
        public string transactionComent { get; set; }


    }
}
