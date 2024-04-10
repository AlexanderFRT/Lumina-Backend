using Lumina_Backend.Models;
using Lumina_Backend.ModelsDTO;

namespace Lumina_Backend.Repository.User
{
    public interface IUserRepository
    {
        Task<Models.User> GetUser(int id);
        Task<Models.User> AddUser(Models.User usr);
        Task<List<Models.User>> GetUsers();
        Task<Login> login(Login login);
    }
}
