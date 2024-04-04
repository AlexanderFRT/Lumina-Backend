using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lumina_BackEnd.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int accountId { get;}
        public int userId { get;}

        public string accountType { get; set; }
        public decimal balance { get; set; }
        public string accountNumber { get; set; }
    }
}
