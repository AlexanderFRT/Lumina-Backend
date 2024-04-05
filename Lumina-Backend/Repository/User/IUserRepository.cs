using Lumina_Backend.Models;

namespace Lumina_Backend.Repository.User
{
    public interface IUserRepository
    {
        Task<Models.User> GetUser(int id);
        Task<Models.User> AddUser(Models.User usr);
        Task<List<Models.User>> GetUsers();
    }
}
