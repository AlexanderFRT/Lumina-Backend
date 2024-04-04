using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lumina_BackEnd.Models
{
    public class Security
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int securityId { get;}
        public int userId { get; }
        public string securityQuestion { get; set; }
        public string securityAnswer { get; set; }


    }
}
