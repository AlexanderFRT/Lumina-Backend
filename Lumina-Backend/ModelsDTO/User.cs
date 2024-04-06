using Lumina_Backend.Models;

namespace Lumina_Backend.ModelsDTO
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; } //Hashed MD5
        public string SessionToken { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string DNI { get; set; }

        public ICollection<Account> Accounts { get; set; }
        public ICollection<Security> Securities { get; set; }
        public ICollection<Log> Logs { get; set; }
    }
}
